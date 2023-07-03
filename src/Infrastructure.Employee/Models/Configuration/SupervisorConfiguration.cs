using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Employee.Models.Configuration
{
    public class SupervisorConfiguration : IEntityTypeConfiguration<SupervisorModel>
    {
        public void Configure(EntityTypeBuilder<SupervisorModel> builder)
        {
            builder.ToTable("Supervisor");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Manager)
                .WithMany(p => p.Supervisors);
            builder.HasMany(p => p.Employees);
        }
    }
}
