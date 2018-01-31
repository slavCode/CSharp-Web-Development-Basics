using System.Collections.Generic;
using HandmadeHTTPServer.Server.Http.Response;

namespace HandmadeHTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;

    public class UserController : Controller
    {
        public IHttpResponse Register() => this.FileViewResponse(@"Users/register");

        public IHttpResponse Login() => this.FileViewResponse("Home/login", new Dictionary<string, string>
        {
            ["showError"] = "none"
        });

        public IHttpResponse Login(IHttpRequest request)
        {
            const string formUsernameKey = "username";
            const string formPasswordKey = "password";

            if (!request.FormData.ContainsKey(formUsernameKey)
                || !request.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            if (string.IsNullOrWhiteSpace(request.FormData[formUsernameKey])
                || string.IsNullOrWhiteSpace(request.FormData[formPasswordKey]))
            {
                return this.FileViewResponse("Home/login", new Dictionary<string, string>
                {
                    ["showError"] = "block",
                    ["error"] = "Please enter valid username and password."
                });
            }

            return new RedirectResponse("/");
        }
    }
}
