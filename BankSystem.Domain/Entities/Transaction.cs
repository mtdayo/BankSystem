using System;
using System.Collections.Generic;
using System.Text;

// 取引のTransactionエンティティ
namespace BankSystem.Domain.Entities
{
    public class Transaction
    {
        public long TransactionId { get; private set; }
        public int AccountId { get; private set; }
        public long? TransferId { get; private set; }

        // 1: 入金 2: 出金 3: 振込
        public byte TransactionType { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Transaction() { } // EF Core用のプライベートコンストラクタ

        public Transaction(int accountId, long? transferId, byte transactionType, decimal amount)
        {
            if (accountId <= 0)
                throw new ArgumentException("口座IDは必須です。");

            if (transactionType < 1 || transactionType > 3)
                throw new ArgumentException("取引種別が不正です。");

            if (amount <= 0)
                throw new ArgumentException("取引額は0円より大きい必要があります。");

            if(transferId.HasValue && transferId <= 0)
                throw new ArgumentException("振込IDが不正です。");

            AccountId = accountId;
            TransferId = transferId;
            TransactionType = transactionType;
            Amount = amount;
        }
    }
}
