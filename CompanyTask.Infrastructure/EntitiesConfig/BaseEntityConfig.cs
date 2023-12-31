﻿using CompanyTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyTask.Infrastructure.EntitiesConfig
{
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired(true).HasColumnType("datetime2");
            builder.Property(x => x.DeletedDate).IsRequired(false).HasColumnType("datetime2");
            builder.Property(x => x.ModifiedDate).IsRequired(false).HasColumnType("datetime2");
            builder.Property(x => x.StatuId).IsRequired(false);
        }
    }
}
