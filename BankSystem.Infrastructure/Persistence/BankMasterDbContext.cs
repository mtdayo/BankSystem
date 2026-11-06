using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Domain.Entities;
using BankSystem.Infrastructure.Configurations;

//BankMasterの専用DbContextを作成する
namespace BankSystem.Infrastructure.Persistence
{
    public class BankMasterDbContext : DbContext
    {
        public BankMasterDbContext(DbContextOptions<BankMasterDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<AccountType> AccountTypes => Set<AccountType>();
        public DbSet<Account> Accounts => Set<Account>();

        //configurationsを適用する
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
