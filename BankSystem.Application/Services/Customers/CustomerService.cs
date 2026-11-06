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

    }
}
