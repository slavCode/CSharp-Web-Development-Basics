namespace FootballBetting.Models
{
    using FootballBetting.Models.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public int Id { get; set; }

        [Required]
        [ValidName]
        public string Name { get; set; }

        public ICollection<CountryContinent> Continents { get; set; } = new List<CountryContinent>();

        public ICollection<Town> Towns { get; set; } = new List<Town>();
    }
}
