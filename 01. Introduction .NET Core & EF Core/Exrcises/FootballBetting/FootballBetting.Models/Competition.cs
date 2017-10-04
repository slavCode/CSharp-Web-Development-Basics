namespace FootballBetting.Models
{
    using System.ComponentModel.DataAnnotations;
    using FootballBetting.Models.Attributes;
    using System.Collections.Generic;

    public class Competition
    {
        public int Id { get; set; }

        [Required]
        [ValidName]
        public string Name { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}