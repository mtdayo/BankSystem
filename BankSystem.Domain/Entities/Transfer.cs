using System;
using System.Collections.Generic;
using System.Text;

//送金のTransferエンティティ
namespace BankSystem.Domain.Entities
{
    public class Transfer
    {
        public long TransferId { get; private set; }
        public int FromAccountId { get; private set; }
        public int ToAccountId { get; private set; }
        public decimal Amount { get; private set; }

        // 1: 処理中 2: 送金完了 3: 送金失敗
        public byte Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Transfer() { } // EF Core用のプライベートコンストラクタ

        public Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            if (fromAccountId <= 0)
                throw new ArgumentException("送金元口座IDは必須です。");

            if (toAccountId <= 0)
                throw new ArgumentException("送金先口座IDは必須です。");

            if (amount <= 0)
                throw new ArgumentException("送金額は0円より大きい必要があります。");

            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
            Status = 1; // 1: 処理中
        }

    }
}
