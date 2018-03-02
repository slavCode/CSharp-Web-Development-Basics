namespace GameStoreApplication.Controllers
{
    using Common;
    using Server.Http.Contracts;
    using Services;

    public class GameController : Controller
    {
        private readonly IGameService games;
        private readonly HtmlDataFill dataFill;

        public GameController(IHttpRequest request)
            : base(request)
        {
            this.games = new GameService();
            this.dataFill = new HtmlDataFill();
        }

        public IHttpResponse Details()
        {
            var id = int.Parse(this.Request.UrlParameters["id"]);

            var model = this.games.FindById(id);

            this.dataFill.Game(this.ViewData, model);
            this.ViewData["id"] = id.ToString();

            if (Authentication.IsAdmin)
            {
                this.ViewData["authDisplay"] = "inline-block";
            }
            else
            {
                this.ViewData["authDisplay"] = "none";
            }


            return this.FileViewResponse(@"game\game-details");
        }
    }
}
