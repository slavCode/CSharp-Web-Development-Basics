namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FootballBetting.Models.Attributes;
    using SocialNetwork.Attributes;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [ValidUsername]
        public string Username { get; set; }

        [Required]
        [ValidPassword]
        public string Password { get; set; }

        [Required]
        [ValidEmail]
        public string Email { get; set; }

        [Required]
        [ValidFullName]
        public string FullName { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; } = new List<Bet>();
    }
}
