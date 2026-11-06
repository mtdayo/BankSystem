using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankSystem.Application.DTOs.Customers
{
    public class CustomerUpdateRequest
    {
        [Required(ErrorMessage = "氏名は必須です。")]
        [StringLength(100, ErrorMessage = "氏名は１００文字以内で指定してください。")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "生年月日は必須です。")]
        public DateOnly? BirthDate { get; set; }

        [Required(ErrorMessage = "住所は必須です。")]
        [StringLength(200, ErrorMessage = "住所は２００文字以内で指定してください。")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "電話番号は必須です。")]
        [StringLength(15, ErrorMessage = "電話番号は１５文字以内で指定してください。")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "メールアドレスは必須です。")]
        [StringLength(254, ErrorMessage = "メールアドレスは２５４文字以内で指定してください。")]
        [EmailAddress(ErrorMessage = "正しいメールアドレスを入力してください。")]
        public string Email { get; set; } = string.Empty;
    }
}
