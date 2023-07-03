using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Employee.Contracts;
using Infrastructure.Employee.Models.Response;
using Infrastructure.Employee;
using Infrastructure.Employee.Models;
using System.Net;
using Microsoft.Extensions.Logging;
namespace Domain.Employee
{
    public class EmployeeReader:IEmployeeReader
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger _logger;
        public EmployeeReader(EmployeeContext employeeContext, ILogger<EmployeeReader> logger)
        {
            _logger = logger;   
            _employeeContext = employeeContext;
        }
        public EmployeeGetResponse Get()
        {
            var response = new EmployeeGetResponse();
            _logger.LogInformation("Fetching Employees..");
            try
            {
                var employees = _employeeContext.Employees.Include(p=>p.Supervisor)
                                                      .Include(emp => emp.Manager).ToList();
                var employeeList = new List<Infrastructure.Employee.Models.Response.Employee>();
                employees.ForEach(emp =>
                {
                    var employee = new Infrastructure.Employee.Models.Response.Employee();
                    employee.FirstName = emp.FirstName;
                    employee.LastName = emp.LastName;
                    employee.Address1 = emp.Address1;
                    if (emp.SupId != null && emp.ManagerId != null)
                    {
                        employee.PayPerHour = emp.PayPerHour;
                    }
                    else if (emp.SupId == null && emp.ManagerId != null)
                    {
                        employee.IsSupervisor = true;
                        employee.AnnualSalary = emp.Supervisor?.AnnualSalary;
                    }
                    else if (emp.SupId == null && emp.ManagerId == null)
                    {
                        employee.IsManager = true;
                        employee.AnnualSalary = emp.Manager?.AnnualSalary;
                        employee.MaxExpenseAmount = emp.Manager?.MaxExpenseAmount;
                    }
                    employeeList.Add(employee);
                });
                _logger.LogInformation("Employees retrieved successfully");
                response.Employees = employeeList;                
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while retrieving employees, Error: " + ex.Message, typeof(EmployeeReader));
                response.Errors.Add(new Error() { Message = ex.Message });
                return response;
            }
            
        }
    }
}
