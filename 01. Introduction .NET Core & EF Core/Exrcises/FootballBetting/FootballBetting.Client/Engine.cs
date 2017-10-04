namespace FootballBetting.Client
{
    using System;
    using FootballBetting.Data;
    using Microsoft.EntityFrameworkCore;

    public class Engine
    {
        public void Run()
        {
            using (var db = new FootballBettingDbContext())
            {

                InitializeDatabase(db);
            }
        }

        private void InitializeDatabase(FootballBettingDbContext db)
        {
            Console.WriteLine("Initializing database...");

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine("Database Ready.");
        }
    }
}
