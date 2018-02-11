using WebServer.ByTheCakeApplication.ViewModels.Account;

namespace WebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Username == username && u.Password == password);
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var profile = db
                    .Users
                    .FirstOrDefault(u => u.Username == username);

                if (profile == null)
                {
                    return null;
                }

                return new ProfileViewModel
                {
                    Username = profile.Username,
                    RegistrationDate = DateTime.UtcNow,
                    OrdersCount = profile.Orders.Count
                };
            }
        }

        public int GetUserId(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var id = db
                    .Users
                    .FirstOrDefault(u => u.Username == username)
                    .Id;

                return id;
            }
        }
    }
}
