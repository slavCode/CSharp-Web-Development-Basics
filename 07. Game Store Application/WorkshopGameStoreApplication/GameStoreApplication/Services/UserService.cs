namespace GameStoreApplication.Services
{
    using Data;
    using Data.Models;
    using System.Linq;
    using ViewModels.Account;

    public class UserService : IUserService
    {
        public bool Create(RegisterUserViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(u => u.Email == model.Email))
                {
                    return false;
                }

                var isFirstUser = !db.Users.Any();

                var user = new User
                {
                    Name = model.FullName,
                    Email = model.Email,
                    Password = model.Password
                };

                if (isFirstUser) user.IsAdmin = true;

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(LoginUserViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db
                    .Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null) return true;

                return false;
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Email == email && u.IsAdmin);
            }
        }
    }
}
