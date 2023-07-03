using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Employee.Models.Response;
namespace Employee.Contracts
{
    public interface IEmployeeReader
    {
        EmployeeGetResponse Get();
    }
}
