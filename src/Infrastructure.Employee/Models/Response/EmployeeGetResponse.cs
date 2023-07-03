using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Employee.Models.Response
{
    public class EmployeeGetResponse:Response
    {
        public List<Employee> Employees { get; set; }
    }
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public decimal? PayPerHour { get; set; }
        public decimal? AnnualSalary { get; set; }
        public decimal? MaxExpenseAmount { get; set; }
        public bool IsManager { get; set; }
        public bool IsSupervisor { get; set; }
    }
}
