using System;
using System.Collections.Generic;
using System.Linq;
using Employee.Contracts;
using Infrastructure.Employee.Models.Request;
using Infrastructure.Employee.Models.Response;
using Infrastructure.Employee;
using Infrastructure.Employee.Models;
using System.Net;
using static Infrastructure.Employee.Constants;
using Microsoft.Extensions.Logging;
namespace Domain.Employee
{
    public class EmployeeAdder:IEmployeeAdd
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger _logger;
        public EmployeeAdder(EmployeeContext employeeContext, ILogger<EmployeeAdder> logger)
        {
            _logger = logger;
            _employeeContext = employeeContext;
        }

        public Response Add(EmployeeAddRequest employeeAddRequest)
        {
            EmployeeModel employee = new EmployeeModel();
            ManagerModel manager = new ManagerModel();
            SupervisorModel supervisor = new SupervisorModel();
            var response = new Response();
            try
            {
                _logger.LogInformation("Adding Employee");
                if (employeeAddRequest.PayPerHour != null && employeeAddRequest.SupId != null && employeeAddRequest.ManagerId != null)
                {
                    employee.FirstName = employeeAddRequest.FirstName;
                    employee.LastName = employeeAddRequest.LastName;
                    employee.Address1 = employeeAddRequest.Address1;
                    employee.PayPerHour = (decimal)employeeAddRequest.PayPerHour;
                    employee.SupId = (int)employeeAddRequest.SupId;
                    employee.ManagerId = (int)employeeAddRequest.ManagerId;
                    _employeeContext.Add(employee);
                    _employeeContext.SaveChanges();
                }
                else if (employeeAddRequest.ManagerId != null && employeeAddRequest.PayPerHour == null && employeeAddRequest.AnnualSalary != null)
                {
                    employee.FirstName = employeeAddRequest.FirstName;
                    employee.LastName = employeeAddRequest.LastName;
                    employee.Address1 = employeeAddRequest.Address1;
                    employee.SupId = null;
                    employee.ManagerId = employeeAddRequest.ManagerId;
                    _employeeContext.Add(employee);
                    _employeeContext.SaveChanges();

                    supervisor.AnnualSalary = (decimal)employeeAddRequest.AnnualSalary;
                    supervisor.ManagerId = (int)employeeAddRequest.ManagerId;
                    supervisor.EmployeeId = _employeeContext.Employees.FirstOrDefault(emp => emp.FirstName == employeeAddRequest.FirstName
                                            && emp.LastName == employeeAddRequest.LastName && emp.Address1 == employeeAddRequest.Address1).Id;
                    _employeeContext.Add(supervisor);
                    _employeeContext.SaveChanges();
                }
                else if (employeeAddRequest.ManagerId == null && employeeAddRequest.SupId == null && employeeAddRequest.PayPerHour == null)
                {
                    employee.FirstName = employeeAddRequest.FirstName;
                    employee.LastName = employeeAddRequest.LastName;
                    employee.Address1 = employeeAddRequest.Address1;
                    employee.SupId = null;
                    employee.ManagerId = null;
                    _employeeContext.Add(employee);
                    _employeeContext.SaveChanges();
                    manager.EmployeeId = _employeeContext.Employees.FirstOrDefault(emp => emp.FirstName == employeeAddRequest.FirstName
                                            && emp.LastName == employeeAddRequest.LastName && emp.Address1 == employeeAddRequest.Address1).Id;
                    manager.AnnualSalary = (decimal)employeeAddRequest.AnnualSalary;
                    manager.MaxExpenseAmount = (decimal)employeeAddRequest.MaxExpenseAmount;
                    _employeeContext.Add(manager);
                    _employeeContext.SaveChanges();
                }
                response.HttpStatusCode = (int)HttpStatusCode.OK;
                response.Message = EmployeeAddedMessage;
                _logger.LogInformation("Employee successfully added");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while adding employee" + ex.Message, typeof(EmployeeAdder));
                response.Errors.Add(new Error() { Message = ex.Message });
                return response;
            }
        }
    }
}
