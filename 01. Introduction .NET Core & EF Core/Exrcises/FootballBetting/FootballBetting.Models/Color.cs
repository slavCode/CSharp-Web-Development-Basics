namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using FootballBetting.Models.Enums;

    public class Color
    {
        public int Id { get; set; }

        public ColorName Name { get; set; }

        public ICollection<Team> TeamPrimaryKits { get; set; } = new List<Team>();

        public ICollection<Team> TeamSecondaryKits { get; set; } = new List<Team>();
    }
}
