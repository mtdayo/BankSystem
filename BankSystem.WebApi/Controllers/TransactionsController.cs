using BankSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly BankTransactionDbContext _db;

        public TransactionsController(BankTransactionDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _db.Transactions.AsNoTracking()
                                               .OrderByDescending(t => t.CreatedAt) //新しい履歴から取得
                                               .Select(t => new
                                               {
                                                   t.TransactionId,
                                                   t.AccountId,
                                                   t.TransferId,
                                                   t.TransactionType,
                                                   t.Amount,
                                                   t.CreatedAt
                                               })
                                               .ToListAsync();
            return Ok(transactions);
        }
    }
}
