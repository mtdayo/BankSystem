using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;


// 取引のTransactionエンティティの設定
namespace BankSystem.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction> //※Transactionクラスは完全修飾子にした方が良いかも
    {
        public void Configure(EntityTypeBuilder<Transaction> builder) //※Transactionクラスは完全修飾子にした方が良いかも
        {
            builder.ToTable("Transactions", table =>
            {
                table.HasCheckConstraint("CK_Transactions_TransactionType", "[TransactionType] IN (1,2,3)");
                table.HasCheckConstraint("CK_Transactions_Amount", "[Amount] > 0");
            });
            builder.HasKey(t => t.TransactionId);
            builder.Property(t => t.TransactionId)
                .UseIdentityColumn();
            builder.Property(t => t.AccountId).
                IsRequired();
            builder.Property(t => t.TransferId)
                .IsRequired(false);
            builder.Property(t => t.TransactionType)
                .IsRequired();
            builder.Property(t => t.Amount)
                .IsRequired()
                .HasPrecision(19, 4);
            builder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
            builder.HasOne<Transfer>()
                .WithMany()
                .HasForeignKey(t => t.TransferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
