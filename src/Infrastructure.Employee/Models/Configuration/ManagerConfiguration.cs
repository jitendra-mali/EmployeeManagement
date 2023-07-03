using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Employee.Models.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<ManagerModel>
    {
        public void Configure(EntityTypeBuilder<ManagerModel> builder)
        {
            builder.ToTable("Manager");
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Employees);
            builder.HasMany(p=>p.Supervisors);
        }
    }
}
