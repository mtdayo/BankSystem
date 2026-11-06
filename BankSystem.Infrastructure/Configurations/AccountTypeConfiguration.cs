using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Infrastructure.Configurations
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("AccountTypes");
            builder.HasKey(a => a.AccountTypeId);
            builder.Property(a => a.AccountTypeId).UseIdentityColumn();
            builder.Property(a => a.TypeName)
                .IsRequired()
                .HasMaxLength(30);
            builder.HasIndex(a => a.TypeName)
                .IsUnique();
        }
    }
}
