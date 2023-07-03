using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Employee.Models.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Supervisor);
            builder.HasOne(p=>p.Manager);
            
        }
    }
}
