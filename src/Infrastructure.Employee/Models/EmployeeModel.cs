using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Employee.Models
{
    public class EmployeeModel
    {
        public EmployeeModel() {}
        public int Id { get; set; }
        public int? SupId { get; set; }
        public int? ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public decimal PayPerHour { get; set; }
        [ForeignKey("SupId")]
        public virtual SupervisorModel Supervisor { get; set; }
        [ForeignKey("ManagerId")]
        public virtual ManagerModel Manager { get; set; }
    }
}
