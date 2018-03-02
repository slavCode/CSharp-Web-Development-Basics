namespace GameStoreApplication
{
    using Controllers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using Services;
    using System;
    using System.Globalization;
    using ViewModels.Account;
    using ViewModels.Admin;

    public class GameStoreApp : IApplication
    {
        private readonly IGameService games = new GameService();

        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add("/");
            appRouteConfig.AnonymousPaths.Add("/account/login");
            appRouteConfig.AnonymousPaths.Add("/account/register");
            appRouteConfig.AnonymousPaths.Add("/account/logout");

            var anonymousGamePaths = this.games.AnonymousGamePaths();
            if (anonymousGamePaths != null)
            {
                foreach (var gamePath in anonymousGamePaths)
                {
                    appRouteConfig.AnonymousPaths.Add(gamePath);
                }
            }

            appRouteConfig
                .Get("account/register",
                    req => new AccountController(req).Register());

            appRouteConfig
                .Post("account/register",
                    req => new AccountController(req).Register(new RegisterUserViewModel
                    {
                        FullName = req.FormData["full-name"],
                        Email = req.FormData["email"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]
                    }));

            appRouteConfig
                .Get("account/login", req => new AccountController(req).Login());

            appRouteConfig
                .Post("account/login",
                    req => new AccountController(req).Login(new LoginUserViewModel
                    {
                        Email = req.FormData["email"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get("account/logout", req => new AccountController(req).Logout());

            appRouteConfig
                .Get("/", req => new HomeController(req).Index());

            appRouteConfig
                .Get("/admin/games/add", req => new AdminController(req).Add());

            appRouteConfig
                .Post("/admin/games/add", req => new AdminController(req).Add(new GameViewModel
                {
                    Title = req.FormData["title"],
                    Description = req.FormData["description"],
                    Image = req.FormData["thumbnail"],
                    Price = decimal.Parse(req.FormData["price"]),
                    Size = double.Parse(req.FormData["size"]),
                    Trailer = req.FormData["videoId"],
                    ReleaseDate = DateTime.ParseExact(req.FormData["release-date"], "yyyy-MM-dd",
                        CultureInfo.InvariantCulture)
                }));

            appRouteConfig
                .Get(@"admin/games/list", req => new AdminController(req).List());

            appRouteConfig
                .Get(@"admin/games/edit/{(?<id>[0-9]+)}", req => new AdminController(req).Edit());

            appRouteConfig
                .Post(@"admin/games/edit/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Edit(
                        new GameViewModel
                        {
                            Id = int.Parse(req.UrlParameters["id"]),
                            Description = req.FormData["description"],
                            Image = req.FormData["thumbnail"],
                            Price = decimal.Parse(req.FormData["price"]),
                            ReleaseDate = DateTime.ParseExact(req.FormData["release-date"], "yyyy-MM-dd",
                                CultureInfo.InvariantCulture),
                            Size = double.Parse(req.FormData["size"]),
                            Title = req.FormData["title"],
                            Trailer = req.FormData["videoId"]

                        }));

            appRouteConfig
                .Get(@"admin/games/delete/{(?<id>[0-9]+)}",
                   req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(@"admin/games/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete());

            appRouteConfig
                .Get(@"games/{(?<id>[0-9]+)}", 
                    req => new GameController(req).Details());
        }
    }
}
