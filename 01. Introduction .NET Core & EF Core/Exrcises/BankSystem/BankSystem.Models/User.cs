using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace BankSystem.Models
{
    using System.Collections.Generic;
    using BankSystem.Models.Attributes;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Length too short!")]
        public string Username { get; set; }

        [Required]
        [ValidPassword]
        public string Password { get; set; }

        [Required]
        [ValidEmail]
        public string Email { get; set; }

        public ICollection<SavingAccount> SavingAccounts { get; set; } = new List<SavingAccount>();

        public ICollection<CheckingAccount> CheckingAccounts { get; set; } = new List<CheckingAccount>();
    }
}
