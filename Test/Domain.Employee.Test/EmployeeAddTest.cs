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
using Infrastructure.Employee.Models.Request;
using static Infrastructure.Employee.Constants;
namespace Domain.Employee.Test
{
    public class EmployeeAddTest
    {
        private EmployeeContext _employeeContext;
        public EmployeeAddTest()
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
        public void EmployeeAdd_Add_ShouldAddEmployee()
        {
            var logger = Substitute.For<ILogger<EmployeeAdder>>();
            var employeeReader = new EmployeeAdder(_employeeContext, logger);
            var empAddRequest = new EmployeeAddRequest
            {
                SupId = 1,
                ManagerId = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address1 = "TestAddress1",
                PayPerHour = 50,
            };
            var result = employeeReader.Add(empAddRequest);
            result.Should().NotBeNull();
            result.Message.Should().Be(EmployeeAddedMessage);
            _employeeContext.Employees.Count(x=>x.FirstName.Equals(empAddRequest.FirstName, StringComparison.OrdinalIgnoreCase)
            && x.LastName.Equals(empAddRequest.LastName,StringComparison.OrdinalIgnoreCase)
            && x.SupId == empAddRequest.SupId && x.ManagerId == empAddRequest.ManagerId).Should().Be(1);    
        }
    }
}
