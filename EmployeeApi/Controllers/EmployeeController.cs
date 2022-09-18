using EmployeeApi.EmployeeDbContext;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly EmployeeContext _employeeContext;
        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> get()
        {
            var data = await _employeeContext.Employee.ToListAsync();
            return data;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee emp)
        {
            if (emp.EmpId is not null)
                return BadRequest("Employee Id Should Be Null");

            await _employeeContext.Employee.AddAsync(emp);
            await _employeeContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee emp)
        {
            var employee = await _employeeContext.Employee.AsNoTracking().Where(a => a.EmpId == emp.EmpId).FirstOrDefaultAsync();
            if (employee is null)
                return BadRequest("employee not found");

            _employeeContext.Employee.Update(emp);
           await _employeeContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeContext.Employee.Where(a => a.EmpId == id).FirstOrDefaultAsync();
            if (employee is null)
                return BadRequest("employee not found");

            _ = _employeeContext.Employee.Remove(employee);
            await  _employeeContext.SaveChangesAsync();
            return Ok();
        }
    }
}
