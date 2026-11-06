using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", table =>
            {
                table.HasCheckConstraint("CK_Accoutns_Status", "[Status] IN (1,2,3)");
            });
            builder.HasKey(a => a.AccountId);
            builder.Property(a => a.AccountId).UseIdentityColumn();
            builder.Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);
            builder.HasIndex(a => a.AccountNumber)
                .IsUnique();
            builder.Property(a => a.CustomerId)
                .IsRequired();
            builder.Property(a => a.AccountTypeId)
                .IsRequired();
            builder.Property(a => a.Status)
                .IsRequired()
                .HasDefaultValue((byte)1);
            builder.Property(a => a.OpenedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
            builder.HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.AccountType)
                .WithMany()
                .HasForeignKey(a => a.AccountTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
