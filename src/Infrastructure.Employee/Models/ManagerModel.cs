using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Employee.Models
{
    public class ManagerModel
    {
        public ManagerModel() { }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal MaxExpenseAmount { get; set; }
        public virtual ICollection<SupervisorModel> Supervisors { get; set; }

        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}