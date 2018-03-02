namespace GameStoreApplication.Services
{
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Admin;

    public class GameService : IGameService
    {
        public void Create(GameViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Title = model.Title,
                    Description = model.Description,
                    Image = model.Image,
                    ReleaseDate = model.ReleaseDate,
                    Size = model.Size,
                    Trailer = model.Trailer,
                    Price = model.Price,
                };

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public IEnumerable<ListGameViewModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games.Select(g => new ListGameViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Price = g.Price,
                        Size = g.Size,
                        Thumbnail = g.Image,
                        Description = g.Description
                    })
                    .ToList();
            }
        }

        public GameViewModel FindById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Games
                    .Where(g => g.Id == id)
                    .Select(g => new GameViewModel
                    {
                        Id = g.Id,
                        Description = g.Description,
                        Size = g.Size,
                        Price = g.Price,
                        ReleaseDate = g.ReleaseDate,
                        Trailer = g.Trailer,
                        Image = g.Image,
                        Title = g.Title
                    })
                    .FirstOrDefault();
            }
        }

        public void Edit(GameViewModel model)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db
                    .Games
                    .FirstOrDefault(g => g.Id == model.Id);

                if (game == null)
                {
                    return;
                }

                game.Description = model.Description;
                game.Image = model.Image;
                game.Price = model.Price;
                game.ReleaseDate = model.ReleaseDate;
                game.Size = model.Size;
                game.Title = model.Title;
                game.Trailer = model.Trailer;

                db.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.FirstOrDefault(g => g.Id == id);

                if (game != null) db.Remove(game);

                db.SaveChanges();
            }
        }

        public List<string> AnonymousGamePaths()
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Games.Any())
                {
                    var result = db
                        .Games
                        .Select(g => $@"/games/{g.Id}")
                        .ToList();

                    return result;
                }

                return null;
            }
        }
    }
}
