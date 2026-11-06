using System;
using System.Collections.Generic;
using System.Text;

// 送金のIdempotencyRecordエンティティ
// 送金の冪等性を保証するためのレコード
namespace BankSystem.Domain.Entities
{
    public class IdempotencyRecord
    {
        public Guid IdempotencyKey { get; private set; }
        public string RequestHash { get; private set; } = string.Empty;
        public string? Result { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private IdempotencyRecord() { } // EF Core用のプライベートコンストラクタ

        public IdempotencyRecord(Guid idempotencyKey, string requestHash)
        {
            if (idempotencyKey == Guid.Empty)
                throw new ArgumentException("冪等性キーは必須です。");

            if (string.IsNullOrWhiteSpace(requestHash))
                throw new ArgumentException("リクエストハッシュは必須です。");

            if (requestHash.Length > 64)
                throw new ArgumentException("リクエストハッシュは64文字以内である必要があります。");

            IdempotencyKey = idempotencyKey;
            RequestHash = requestHash;
        }
    }
}
