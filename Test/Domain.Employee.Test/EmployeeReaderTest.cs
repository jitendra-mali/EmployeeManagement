using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Infrastructure.Employee;
using Infrastructure.Employee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Employee.Contracts;
using FluentAssertions;
using Domain.Employee;
namespace Domain.Employee.Test
{
    public class EmployeeReaderTest
    {
        private EmployeeContext _employeeContext;
        public EmployeeReaderTest()
        {
            InitContext();
        }
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<EmployeeContext>().UseInMemoryDatabase("Employee");
            var context = new EmployeeContext(builder.Options);
            var employees = new List<EmployeeModel>()
            {
                new EmployeeModel { Id=1, SupId=null, ManagerId=null,FirstName="TestEmp1FName",LastName="TestEmp1LName",Address1="TestEmp1Address1"},
                new EmployeeModel { Id=2 ,SupId=null, ManagerId=1,FirstName="TestEmp2FName",LastName="TestEmp2LName",Address1="TestEmp1Address2"},
                new EmployeeModel { Id=3, SupId=1, ManagerId=1,FirstName="TestEmp3FName",LastName="TestEmp3LName",Address1="TestEmp1Address3",PayPerHour=50},
            };
            var managers = new List<ManagerModel>()
            {
                new ManagerModel { Id=1, EmployeeId = 1, AnnualSalary = 150000, MaxExpenseAmount = 1000}

            };
            var supervisors = new List<SupervisorModel>()
            {
                new SupervisorModel{ Id=1,EmployeeId = 2, ManagerId = 1, AnnualSalary = 100000}
            };
            context.Employees.AddRange(employees);
            context.Managers.AddRange(managers);
            context.Supervisors.AddRange(supervisors);
            context.SaveChanges();
            _employeeContext = context;
        }
        [Fact]
        public void EmployeeReader_Get_ShouldReturnEmployees()
        {
            var logger = Substitute.For<ILogger<EmployeeReader>>();
            var employeeReader = new EmployeeReader(_employeeContext, logger);
            var result = employeeReader.Get();
            result.Should().NotBeNull();
            result.Employees.Should().HaveCount(3);
        }
    }
}
