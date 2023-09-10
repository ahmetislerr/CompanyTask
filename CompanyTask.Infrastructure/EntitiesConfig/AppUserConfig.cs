using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(1);

            builder.Property(x => x.FirstName)
                .IsRequired(true)
                .HasMaxLength(30)
                .IsUnicode(true)
                .HasColumnOrder(2);

            builder.Property(x => x.LastName)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnOrder(3);

            builder.Property(x => x.BirthDate)
                .IsRequired(false)
                .HasColumnType("date")
                .HasColumnOrder(4);

            builder.Property(x => x.ManagerId)
                .IsRequired(false)
                .HasColumnOrder(5);

            builder.Property(x => x.UserName)
                .HasMaxLength(30).HasColumnOrder(6);


            //Foreign Key one to many
            builder.HasOne(x => x.Manager)
                    .WithMany(x => x.Employees)
                    .HasForeignKey(x => x.ManagerId);

            base.Configure(builder);
        }
    }
}
