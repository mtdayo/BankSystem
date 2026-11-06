using BankSystem.Application.DTOs.Customers;
using BankSystem.Application.Interfaces.Customers;
using BankSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerServie)
        {
            _customerService = customerServie;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();

            return Ok(customers);
        }

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetById(int customerId)
        {
            var customer = await _customerService.GetByIdAsync(customerId);

            if (customer is null) return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateRequest request)
        {
            var customer = await _customerService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { customerId = customer.CustomerId },
                customer

            );
        }

        [HttpPut("{customerId:int}")]
        public async Task<IActionResult> Update(int customerId, CustomerUpdateRequest request)
        {
            var customer = await _customerService.UpdateAsync(customerId, request);

            if (customer is null) return NotFound();

            return Ok(customer);
        }

    }
}
