using BankSystem.Application.Interfaces.Customers;
using BankSystem.Infrastructure.Persistence;
using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


//データベースにアクセスするCustomer用のクラス
namespace BankSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankMasterDbContext _db;
        public CustomerRepository(BankMasterDbContext db)
        {
            _db = db;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _db.Customers.AsNoTracking()
                                      .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _db.Customers.AsNoTracking()
                                      .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
        public async Task AddAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _db.Accounts.Where(a => a.CustomerId == customerId)
                                     .ToListAsync();
        }

        public async Task UpdateAccountsAsync(List<Account> accounts)
        {
            _db.Accounts.UpdateRange(accounts);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
        }
    }
}
