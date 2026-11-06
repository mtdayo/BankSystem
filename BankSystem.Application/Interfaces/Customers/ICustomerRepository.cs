using BankSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

//infrastructure層に顧客データを取得するリポジトリ
namespace BankSystem.Application.Interfaces.Customers
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int customerId);
        
        Task AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task<List<Account>> GetAccountsByCustomerIdAsync(int customerId);

        Task UpdateAccountsAsync(List<Account> accounts);

        Task UpdateCustomerAsync(Customer customer);

        Task SaveChangesAsync();

        Task<bool> ExistsByCustomerNumberAsync(string customerNumber);
    }
}
