using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Employee.Models.Request
{
    public class EmployeeAddRequest
    {
        public int? SupId { get; set; }
        public int? ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public decimal? PayPerHour { get; set; }
        public decimal? AnnualSalary { get; set; }
        public decimal? MaxExpenseAmount { get; set; }
    }
}
