using BankSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountBalancesController : ControllerBase
    {
        private readonly BankTransactionDbContext _db;

        public AccountBalancesController(BankTransactionDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var balances = await _db.AccountBalances.AsNoTracking()
                                                    .Select(ab => new
                                                    {
                                                        ab.AccountId,
                                                        ab.Balance,
                                                        ab.UpdatedAt
                                                    })
                                                    .ToListAsync();

            return Ok(balances);
        }
    }
}
