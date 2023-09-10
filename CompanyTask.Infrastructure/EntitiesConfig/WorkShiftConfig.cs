using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public class WorkShiftConfig : BaseEntityConfig<WorkShift>
    {
        public void Configure(EntityTypeBuilder<WorkShift> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnOrder(2);

            builder.Property(x => x.Date)
                .IsRequired(true)
                .HasColumnType("Date")
                .HasColumnOrder(3);

            builder.Property(x => x.StartTime)
                .IsRequired(true)
                .HasColumnType("Date")
                .HasColumnOrder(4);

            builder.Property(x => x.EndTime)
                .IsRequired(true)
                .HasColumnType("Date")
                .HasColumnOrder(5);

            builder.HasOne(x => x.User)
                .WithMany(x => x.WorkShifts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);

        }
    }
}
