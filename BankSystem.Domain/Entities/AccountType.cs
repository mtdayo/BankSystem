using System;
using System.Collections.Generic;
using System.Text;

//口座種別マスタのAccountTypeエンティティ
namespace BankSystem.Domain.Entities
{
    public class AccountType
    {
        public int AccountTypeId { get; private set; }
        public string TypeName { get; private set; } = string.Empty;

        private AccountType() { } // EF Core用のプライベートコンストラクタ

        public AccountType(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentException("口座種別名は必須です。");
            if (typeName.Length > 30)
                throw new ArgumentException("口座種別名は30文字以内である必要があります。");
            TypeName = typeName;
        }
    }
}
