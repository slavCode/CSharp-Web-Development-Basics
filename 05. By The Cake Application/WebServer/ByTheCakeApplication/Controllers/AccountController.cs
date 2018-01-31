namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";
            
            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formUsernameKey = "username";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formUsernameKey)
                || !req.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var username = req.FormData[formUsernameKey];
            var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["errorMessage"] = "block";
                this.ViewData["error"] = "Invalid username or password.";

                return this.FileViewResponse(@"account\login");
            }

            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
            
            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/");
        }
    }
}