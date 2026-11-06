using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Infrastructure.Configurations
{
    public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<IdempotencyRecord>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdempotencyRecord> builder)
        {
            builder.ToTable("IdempotencyRecords");
            builder.HasKey(ir => ir.IdempotencyKey);
            builder.Property(ir => ir.IdempotencyKey)
                .ValueGeneratedNever();
            builder.Property(ir => ir.RequestHash)
                .IsRequired()
                .HasMaxLength(64)
                .IsUnicode(false);
            builder.Property(ir => ir.Result)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");
            builder.Property(ir => ir.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
        }
    }
}
