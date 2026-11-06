using BankSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseCheckController : ControllerBase
    {
        private readonly BankMasterDbContext _masterDb;
        private readonly BankTransactionDbContext _transactionDb;
        public DatabaseCheckController(BankMasterDbContext masterDb, BankTransactionDbContext transactionDb)
        {
            _masterDb = masterDb;
            _transactionDb = transactionDb;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var masterConnected = await _masterDb.Database.CanConnectAsync();
            var transactionConnected = await _transactionDb.Database.CanConnectAsync();

            return Ok(new
            {
                BankMasterDB = masterConnected,
                BankTransactionDB = transactionConnected
            });
        }
    }
}
