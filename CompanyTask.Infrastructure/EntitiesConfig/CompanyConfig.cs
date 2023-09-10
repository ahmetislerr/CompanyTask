using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public class CompanyConfig : BaseEntityConfig<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(1);

            builder.Property(x => x.CompanyName)
                .IsRequired(true)
                .HasColumnOrder(2);

            builder.Property(x => x.TaxNumber)
                .IsRequired(true)
                .HasColumnOrder(3);

            builder.Property(x => x.PhoneNumber)
                .IsRequired(true)
                .HasColumnOrder(4);

            builder.Property(x => x.NumberOfEmployee)
                .IsRequired(true)
                .HasColumnOrder(5);

            builder.Property(x => x.ImagePath)
                .IsRequired(false)
                .HasColumnOrder(6);

            builder.Property(x => x.ActivationDate)
                .IsRequired(true)
                .HasColumnOrder(7);

            // Foreign Key

            builder.HasMany(x => x.Departments)
               .WithOne(x => x.Company)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Titles)
               .WithOne(x => x.Company)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Users)
               .WithOne(x => x.Company)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CompanySector)
               .WithMany(x => x.Companies)
               .HasForeignKey(x => x.CompanySectorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CompanyRepresentative)
                .WithOne(x => x.CompanyRepresentative)
                .HasForeignKey<Company>(x => x.CompanyRepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
