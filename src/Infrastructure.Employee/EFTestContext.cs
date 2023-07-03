using Microsoft.EntityFrameworkCore;
using Infrastructure.Employee.Models;
using Infrastructure.Employee.Models.Configuration;
namespace Infrastructure.Employee
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {
            
        }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<SupervisorModel> Supervisors { get; set; }
        public DbSet<ManagerModel> Managers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new SupervisorConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        }
    }
}
