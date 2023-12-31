﻿using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnOrder(2);


            builder.Property(x => x.CityId)
                .IsRequired(true)
                .HasColumnOrder(3);

            //Foreign Key one to many

            builder.HasMany(x => x.Addresses)
                .WithOne(x => x.District)
                .HasForeignKey(x => x.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
