using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public class StatuConfig : IEntityTypeConfiguration<Statu>
    {
        public void Configure(EntityTypeBuilder<Statu> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);

            //Foreign Key one to many

            builder.HasMany(x => x.AppUsers)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Addresses)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Departments)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Titles)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Companies)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
