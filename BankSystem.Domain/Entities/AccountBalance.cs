using System;
using System.Collections.Generic;
using System.Text;

//口座残高のAccountBalanceエンティティ
namespace BankSystem.Domain.Entities
{
    public class AccountBalance
    {
        public int AccountId { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private AccountBalance() { } // EF Core用のプライベートコンストラクタ

        public AccountBalance(int accountId, decimal balance = 0)
        {
            if (accountId <= 0)
                throw new ArgumentException("口座IDは必須です。");
            if (balance < 0)
                throw new ArgumentException("残高は0円以上である必要があります。");

            AccountId = accountId;
            Balance = balance;
        }
    }
}
