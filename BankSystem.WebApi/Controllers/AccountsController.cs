using BankSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly BankMasterDbContext _db;

        public AccountsController(BankMasterDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _db.Accounts
                                .AsNoTracking()
                                //.Include(a => a.Customer)
                                //.Include(a => a.AccountType)
                                .Select(a => new
                                {
                                    a.AccountId,
                                    a.AccountNumber,
                                    CustomerName = a.Customer.Name,
                                    AccountType = a.AccountType.TypeName,
                                    a.Status,
                                    a.OpenedAt
                                })
                                .ToListAsync();
            return Ok(accounts);
        }
    }
}
