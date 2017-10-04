namespace FootballBetting.Data
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;

    public class FootballBettingDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<ResultPrediction> ResultPredictions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<BetGame> BetGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=Y510P;Database=FootballBetting;Integrated Security=True;");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Team>()
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.TeamPrimaryKits)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.TeamSecondaryKits)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasOne(t => t.Town)
                .WithMany(to => to.Teams)
                .HasForeignKey(t => t.TownId);

            builder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CountryContinent>()
                .HasKey(cc => new
                {
                    cc.CountryId,
                    cc.ContinentId
                });

            builder.Entity<Country>()
                .HasMany(cou => cou.Continents)
                .WithOne(cc => cc.Country)
                .HasForeignKey(cou => cou.CountryId);

            builder.Entity<Continent>()
                .HasMany(con => con.Countries)
                .WithOne(cc => cc.Continent)
                .HasForeignKey(con => con.ContinentId);

            builder.Entity<Country>()
                .HasMany(cou => cou.Towns)
                .WithOne(to => to.Country)
                .HasForeignKey(cou => cou.CountryId);

            builder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            builder.Entity<Player>()
                .HasOne(p => p.Position)
                .WithMany(pos => pos.Players)
                .HasForeignKey(p => p.PositionId);

            builder.Entity<PlayerStatistic>()
                .HasKey(ps => new
                {
                    ps.GameId,
                    ps.PlayerId
                });

            builder.Entity<Player>()
                .HasMany(p => p.PlayerStatistics)
                .WithOne(ps => ps.Player)
                .HasForeignKey(p => p.PlayerId);

            builder.Entity<Game>()
                .HasOne(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);

            builder.Entity<Game>()
                .HasOne(g => g.Competition)
                .WithMany(com => com.Games)
                .HasForeignKey(g => g.CompetitionId);

            builder.Entity<BetGame>()
                .HasKey(bg => new
                {
                    bg.BetId,
                    bg.GameId
                });

            builder.Entity<BetGame>()
                .HasOne(bg => bg.ResultPrediction)
                .WithMany(rp => rp.BetGames)
                .HasForeignKey(bg => bg.ResultPredictionId);

            builder.Entity<Bet>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId);

            base.OnModelCreating(builder);
        }
    }
}
