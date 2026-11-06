using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId).UseIdentityColumn();

            builder.Property(c => c.CustomerNumber)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.HasIndex(c => c.CustomerNumber).IsUnique();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(254)
                .IsUnicode();

            builder.Property(c => c.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
