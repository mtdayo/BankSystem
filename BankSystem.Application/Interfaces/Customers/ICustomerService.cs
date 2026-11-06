using System;
using System.Collections.Generic;
using System.Text;
using BankSystem.Application.DTOs.Customers;
using BankSystem.Domain.Entities;

//顧客に関する処理の契約
namespace BankSystem.Application.Interfaces.Customers
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(int customerId);

        Task<CustomerDto> CreateAsync(CustomerCreateRequest request);

        Task<CustomerDto?> UpdateAsync(int customerId, CustomerUpdateRequest request);
    }
}
