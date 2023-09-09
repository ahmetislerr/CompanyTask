using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50)
                .HasColumnOrder(2);

            // Foreign Key one to many
            builder.HasMany(x => x.Users)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);

        }
    }
}
