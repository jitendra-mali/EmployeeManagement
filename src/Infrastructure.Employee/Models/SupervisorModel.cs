using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Employee.Models
{
    public class SupervisorModel
    {
        public SupervisorModel() { }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public decimal AnnualSalary { get; set; }
        [ForeignKey("ManagerId")]
        public virtual ManagerModel Manager { get; set; }
        
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}
