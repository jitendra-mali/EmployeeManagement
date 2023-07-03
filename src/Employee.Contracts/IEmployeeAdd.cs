using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Employee.Models.Response;
using Infrastructure.Employee.Models.Request;
namespace Employee.Contracts
{
    public interface IEmployeeAdd
    {
        Response Add(EmployeeAddRequest employeeAddRequest);
    }
}
