namespace FootballBetting.Models
{
    using FootballBetting.Models.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public int Id { get; set; }

        [Required]
        [ValidName]
        public string Name { get; set; }

        public byte[] Logo { get; set; }

        public string Initials => this.Name.ToUpper().Substring(0, 2);

        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }

        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public decimal Budget { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public ICollection<Game> HomeGames { get; set; } = new List<Game>();

        public ICollection<Game> AwayGames { get; set; } = new List<Game>();
    }
}
