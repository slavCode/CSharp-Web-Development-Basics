namespace BankSystem.Client.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BankSystem.Data;
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class Engine
    {
        public void Run()
        {
            using (var db = new BankSystemDbContext())
            {
                InitializeDatabase(db);

                var input = Console.ReadLine();

                while (input != "End")
                {
                    var inputArgs = input.Split().ToList();
                    var command = inputArgs[0];
                    inputArgs.RemoveAt(0);

                    switch (command)
                    {
                        case "Register":
                            RegisterUser(db, inputArgs);
                            break;
                    }

                    input = Console.ReadLine();
                }
            }
        }



        private void InitializeDatabase(BankSystemDbContext db)
        {
            Console.WriteLine("Initializing database...");

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine("Database Ready.");
        }

        private void RegisterUser(BankSystemDbContext db, List<string> inputArgs)
        {
            
            var username = inputArgs[0];
            var password = inputArgs[1];
            var email = inputArgs[2];

            var user = new User
            {
                Username = username,
                Password = password,
                Email = email
            };

            db.Users.Add(user);
            db.SaveChanges();

            Console.WriteLine($"User {username} is registered in the database.");
        }
    }
}
