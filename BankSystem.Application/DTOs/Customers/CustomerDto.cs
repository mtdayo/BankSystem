using System;
using System.Collections.Generic;
using System.Text;

//Customers用のDTO
namespace BankSystem.Application.DTOs.Customers
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

    }
}
