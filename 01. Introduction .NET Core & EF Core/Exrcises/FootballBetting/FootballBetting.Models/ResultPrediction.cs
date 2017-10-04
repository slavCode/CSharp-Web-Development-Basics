namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using FootballBetting.Models.Enums;

    public class ResultPrediction
    {
        public int Id { get; set; }

        public Prediction Prediction { get; set; }

        public ICollection<BetGame> BetGames { get; set; } = new List<BetGame>();
    }
}