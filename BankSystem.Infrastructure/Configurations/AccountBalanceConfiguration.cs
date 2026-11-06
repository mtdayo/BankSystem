using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Infrastructure.Configurations
{
    public class AccountBalanceConfiguration : IEntityTypeConfiguration<AccountBalance>
    {
        public void Configure(EntityTypeBuilder<AccountBalance> builder)
        {
            builder.ToTable("AccountBalances", table =>
            {
                table.HasCheckConstraint("CK_AccountBalance_Balance", "[Balance] >= 0");
            });

            builder.HasKey(ab => ab.AccountId);
            builder.Property(ab => ab.AccountId)
                .ValueGeneratedNever();
            builder.Property(ab => ab.Balance)
                .IsRequired()
                .HasPrecision(19, 4)
                .HasDefaultValue(0m);
            builder.Property(ab => ab.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
