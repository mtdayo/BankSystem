using BankSystem.Domain.Entities;
using BankSystem.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

//BankTransactionの専用DbContextを作成する
namespace BankSystem.Infrastructure.Persistence
{
    public class BankTransactionDbContext : DbContext
    {
        public BankTransactionDbContext(DbContextOptions<BankTransactionDbContext> options) : base(options)
        {
        }

        public DbSet<AccountBalance> AccountBalances => Set<AccountBalance>();

        public DbSet<Transfer> Transfers => Set<Transfer>();

        public DbSet<Transaction> Transactions => Set<Transaction>();

        public DbSet<IdempotencyRecord> IdempotencyRecords => Set<IdempotencyRecord>();

        //configurationsを適用する
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new TransferConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new IdempotencyRecordConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
