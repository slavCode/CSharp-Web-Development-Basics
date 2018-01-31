namespace HandmadeHTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        public IHttpResponse Index() => this.FileViewResponse(@"home\index");

        public IHttpResponse AboutUs() => this.FileViewResponse(@"home\aboutUs");

        public IHttpResponse Login() => this.FileViewResponse(@"home\login", new Dictionary<string, string>
        {
            ["showResult"] = "none",
            ["showMessage"] = "none"
        });
    }
}
