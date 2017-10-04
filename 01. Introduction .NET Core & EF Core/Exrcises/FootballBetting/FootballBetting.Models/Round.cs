namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using FootballBetting.Models.Enums;

    public class Round
    {
        public int Id { get; set; }

        public RoundName Name { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}