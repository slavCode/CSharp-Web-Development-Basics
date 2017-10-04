namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FootballBetting.Models.Attributes;

    public class Position
    {
        [Key]
        [ValidPositionId]
        public string Id { get; set; }

        public string PositionDescription
        {
            get
            {
                switch (this.Id)
                {
                    case "GK":
                        return "Goalkeeper";

                    case "DF":
                        return "Defender";

                    case "MF":
                        return "Midfielder";

                    case "FW":
                        return "Forward";
                }

                return "N/A";
            }

            set { this.PositionDescription = value; }
        }

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}