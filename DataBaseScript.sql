CREATE DATABASE EmployeeDb  
GO  
  
USE [EmployeeDb]  
GO   
SET ANSI_NULLS ON  
GO  
  
SET QUOTED_IDENTIFIER ON  
GO  
  
SET ANSI_PADDING ON  
GO  
  
CREATE TABLE [dbo].[Employee](  
    [EmpId] [int] IDENTITY(1,1) NOT NULL,  
    [EmpName] [varchar](50) NOT NULL,  
    [EmpContact] [varchar](50) NOT NULL,  
    [EmpEmail] [varchar](50) NOT NULL,  
    [EmpAddress] [varchar](250) NULL,  
PRIMARY KEY CLUSTERED   
(  
    [EmpId] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  
  
GO  
  
SET ANSI_PADDING ON  
GO  
