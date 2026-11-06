using System;
using System.Collections.Generic;
using System.Text;

//BankMasterDB.CustomersのCustomerエンティティ
namespace BankSystem.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string CustomerNumber { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public DateOnly BirthDate { get; private set; }
        public string Address { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        public bool IsDeleted { get; private set; }

        private Customer() { } // EF Core用のプライベートコンストラクタ

        public Customer(string customerNumber, string name, DateOnly birthDate, string address, string phoneNumber, string email)
        {
            if (string.IsNullOrWhiteSpace(customerNumber))
                throw new ArgumentException("顧客番号は必須です。");

            if (customerNumber.Length > 12)
                throw new ArgumentException("顧客番号は12文字以内である必要があります。");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("名前は必須です。");

            if (name.Length > 100)
                throw new ArgumentException("氏名は100文字以内である必要があります。");

            if (birthDate > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("生年月日は未来の日付であってはなりません。");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("住所は必須です。");

            if (address.Length > 200)
                throw new ArgumentException("住所は200文字以内である必要があります。");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("電話番号は必須です。");

            if (phoneNumber.Length > 15)
                throw new ArgumentException("電話番号は15文字以内である必要があります。");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("メールアドレスは必須です。");

            if (email.Length > 254)
                throw new ArgumentException("メールアドレスは254文字以内である必要があります。");

            CustomerNumber = customerNumber;
            Name = name;
            BirthDate = birthDate;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            CreatedAt = DateTime.Now;
        }

        //顧客情報を更新するメソッド
        public void Update(string name, DateOnly birthDate, string address, string phoneNumber, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("氏名は必須です。");
            if (name.Length > 100)
                throw new ArgumentException("氏名は１００文字以内で指定してください。");

            if (birthDate > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("生年月日は未来の日付にできません。");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("住所は必須です");
            if (address.Length > 200)
                throw new ArgumentException("住所は２００文字以内で指定してください。");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("電話番号は必須です。");
            if (phoneNumber.Length > 15)
                throw new ArgumentException("電話番号は１５文字以内で指定してください。");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("メールアドレスは必須です。");
            if (email.Length > 254)
                throw new ArgumentException("メールアドレスは２５４文字以内で指定してください。");

            Name = name;
            BirthDate = birthDate;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        //顧客情報を論理削除するメソッド
        public void Delete()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException("この顧客は既に削除されています。");
            }

            IsDeleted = true;
        }
    }
}
