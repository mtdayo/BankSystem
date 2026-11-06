using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Infrastructure.Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers", table =>
            {
                table.HasCheckConstraint("CK_Transfers_Amount", "[Amount] > 0");
                table.HasCheckConstraint("CK_Transfers_Status", "[Status] IN (1,2,3)");
                table.HasCheckConstraint("CK_Transfers_Accounts", "[FromAccountId] <> [ToAccountId]");
            });
            builder.HasKey(t => t.TransferId);
            builder.Property(t => t.TransferId)
                .UseIdentityColumn();
            builder.Property(t => t.FromAccountId)
                .IsRequired();
            builder.Property(t => t.ToAccountId)
                .IsRequired();
            builder.Property(t => t.Amount)
                .IsRequired()
                .HasPrecision(19, 4);
            builder.Property(t => t.Status)
                .IsRequired()
                .HasDefaultValue((byte)1);
            builder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
        }
    }
}
