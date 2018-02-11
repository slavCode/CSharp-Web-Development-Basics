namespace WebServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using ViewModels.Account;
    using ViewModels.Products;


    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/calculator",
                    req => new CalculatorController().Calculate());

            appRouteConfig
                .Post("calculator",
                    req => new CalculatorController().Calculate(
                        req.FormData["number1"], req.FormData["number2"], req.FormData["mathOperator"]));

            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());

            appRouteConfig
                .Get("/add", req => new ProductsController().Add());

            appRouteConfig
                .Post("/add",
                    req => new ProductsController().Add(req, new ProductViewModel
                    {
                        Name = req.FormData["name"],
                        Price = decimal.Parse(req.FormData["price"]),
                        PictureUrl = req.FormData["pictureUrl"]
                    }));
            
            appRouteConfig
                .Get("/search",
                 req => new ProductsController().Search(req));

            appRouteConfig
                .Get("/register",
                req => new AccountController().Register());

            appRouteConfig
                .Post("/register",
                req => new AccountController().Register(req, new RegisterUserViewModel
                {
                    Username = req.FormData["username"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirm-password"]
                }));
            
            appRouteConfig
                .Get("/login", req => new AccountController().Login());

            appRouteConfig
                .Post("/login", req => new AccountController().Login(
                    req,
                    new LoginViewModel
                    {
                        Username = req.FormData["username"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get("profile", req => new AccountController().Profile(req));

            appRouteConfig
                .Post("/logout", req => new AccountController().Logout(req));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}",
                 req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get("/cart", req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post("/shopping/finish-order", req => new ShoppingController().FinishOrder(req));

            appRouteConfig
                .Get("/cakes/{(?<id>[0-9]+)}",
                 req => new ProductsController().Details(req));

            appRouteConfig
                .Get("/orders", req => new OrdersController().Orders(req));

            // TODO
            // OrderDetails
            //appRouteConfig
            //    .Get("/orderDetails/{(?<id>[0-9]+)}", req => new OrdersController().OrderDetails(req));
        }
    }
}
