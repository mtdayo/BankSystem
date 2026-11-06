using System;
using System.Collections.Generic;
using System.Text;

//口座マスタのAccountエンティティ
namespace BankSystem.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; private set; }
        public string AccountNumber { get; private set; } = string.Empty;
        public int CustomerId { get; private set; }
        public int AccountTypeId { get; private set; }
        public byte Status { get; private set; }
        public DateTime OpenedAt { get; private set; }

        //ナビゲーションプロパティ
        public Customer Customer { get; private set; } = null!;
        public AccountType AccountType { get; private set; } = null!;

        private Account() { } // EF Core用のプライベートコンストラクタ

        public Account(string accountNumber, int customerId, int accountTypeId)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("口座番号は必須です。");

            if (accountNumber.Length > 12)
                throw new ArgumentException("口座番号は12文字以内である必要があります。");

            if (customerId <= 0)
                throw new ArgumentException("顧客IDは必須です。");

            if (accountTypeId <= 0)
                throw new ArgumentException("口座種別IDは必須です。");

            AccountNumber = accountNumber;
            CustomerId = customerId;
            AccountTypeId = accountTypeId;
            Status = 1; // 1: 有効 2: 凍結 3: 解約

        }

        public void close()
        {
            if (Status == 3)
            {
                throw new InvalidOperationException("この口座は既に解約されています。");
            }
        }

    }
}
