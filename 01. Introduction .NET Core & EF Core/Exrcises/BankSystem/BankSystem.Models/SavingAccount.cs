namespace BankSystem.Models
{
    public class SavingAccount
    {
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public double InterestRate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
