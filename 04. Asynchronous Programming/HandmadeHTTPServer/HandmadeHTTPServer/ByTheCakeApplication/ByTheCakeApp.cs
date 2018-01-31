namespace HandmadeHTTPServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().AboutUs());

            appRouteConfig
                .Get("/login", req => new UserController().Login());

            appRouteConfig
                .Post("/login", req => new UserController().Login(req));

            appRouteConfig
                .Get("/add", req => new CakesController().Add());

            appRouteConfig
                .Post("/add", req => new CakesController().Add(req.FormData["name"], req.FormData["price"]));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req));

            appRouteConfig
                .Get("/calculator", req => new CalculatorController().Calculator());

            appRouteConfig
                .Post("/calculator", req => new CalculatorController().Calculator(req));

            appRouteConfig
                .Get("/email", req => new EmailController().Email());

            appRouteConfig
                .Post("/email", req => new EmailController()
                .Email(
                   req.FormData["email"],
                   req.FormData["subjectName"],
                   req.FormData["message"]));

            appRouteConfig
                .Get("/register", req => new UserController().Register());
        }
    }
}
