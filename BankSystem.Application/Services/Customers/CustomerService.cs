using BankSystem.Application.DTOs.Customers;
using BankSystem.Application.Interfaces.Customers;
using BankSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BankSystem.Application.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerNumber = c.CustomerNumber,
                Name = c.Name,
                BirthDate = c.BirthDate,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                CreatedAt = c.CreatedAt
            })
            .ToList();
        }

        public async Task<CustomerDto?> GetByIdAsync(int customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);

            if (customer is null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerNumber = customer.CustomerNumber,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreateRequest request)
        {
            //顧客番号重複チェック
            if (await _repository.ExistsByCustomerNumberAsync(request.CustomerNumber))
            {
                throw new InvalidOperationException("顧客番号が重複しています。");
            }

            var customer = new Customer(
                request.CustomerNumber,
                request.Name,
                request.BirthDate!.Value,
                request.Address,
                request.PhoneNumber,
                request.Email
            );

            await _repository.AddAsync(customer);

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerNumber = customer.CustomerNumber,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task<CustomerDto?> UpdateAsync(int customerId, CustomerUpdateRequest request)
        {
            var customer = await _repository.GetByIdAsync(customerId);

            if (customer is null) return null;

            customer.Update(
                request.Name,
                request.BirthDate!.Value,
                request.Address,
                request.PhoneNumber,
                request.Email
            );

            await _repository.UpdateAsync(customer);

            await _repository.SaveChangesAsync();

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerNumber = customer.CustomerNumber,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            //顧客を取得
            var customer = await _repository.GetByIdAsync(customerId);

            if (customer is null) return false;

            //既に削除済みなら処理しない
            if (customer.IsDeleted) return false;

            //顧客を論理削除
            customer.Delete();

            //顧客に紐づく口座を取得
            var accounts = await _repository.GetAccountsByCustomerIdAsync(customerId);

            //口座を解約状態に変更
            foreach (var account in accounts)
            {
                if (account.Status != 3) account.close();
            }

            //口座情報を更新
            await _repository.UpdateAccountsAsync(accounts);

            //顧客情報を更新
            await _repository.UpdateCustomerAsync(customer);

            await _repository.SaveChangesAsync();

            return true;
        }

    }
}
