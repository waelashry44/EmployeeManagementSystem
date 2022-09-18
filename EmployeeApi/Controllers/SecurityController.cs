using EmployeeApi.EmployeeDbContext;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IOptions<MyConfig> _config;
        public SecurityController(IOptions<MyConfig> config)
        {
            _config = config;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] User user)
        {
            if (user.UserName == "admin" && user.Password == "admin123")
            {
                var issuer = _config.Value.Issuer;
                var audience = _config.Value.Audience;
                var key = Encoding.ASCII.GetBytes(_config.Value.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();
        }
    }
}
