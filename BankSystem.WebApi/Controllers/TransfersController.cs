using BankSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransfersController : ControllerBase
    {
        private readonly BankTransactionDbContext _db;

        public TransfersController(BankTransactionDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transfers = await _db.Transfers.AsNoTracking()
                                               .OrderByDescending(t => t.CreatedAt)
                                               .Select(t => new
                                               {
                                                   t.TransferId,
                                                   t.FromAccountId,
                                                   t.ToAccountId,
                                                   t.Amount,
                                                   t.Status,
                                                   t.CreatedAt
                                               })
                                               .ToListAsync();

            return Ok(transfers);
        }
    }
}
