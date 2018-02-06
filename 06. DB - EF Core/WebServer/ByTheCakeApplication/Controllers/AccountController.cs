namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using ViewModels;
    using Services;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly UserService users = new UserService();

        public IHttpResponse Register()
        {
            SetDefaultViewData();
            return FileViewResponse(@"account/register");
        }

        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            if (model.Username.Length < 3
                || model.Password.Length > 20
                || model.ConfirmPassword != model.Password)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid user details.";

                return FileViewResponse(@"account\register");
            }

            var success = this.users.Create(model.Username, model.Password);

            if (success)
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }

            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "This username is taken";

                return FileViewResponse(@"account\register");
            }
        }

        public IHttpResponse Login()
        {
            SetDefaultViewData();
            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req, LoginUserViewModel model)
        {
            var username = model.Username;
            var password = model.Password;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["errorMessage"] = "block";
                this.ViewData["error"] = "Invalid username or password.";

                return this.FileViewResponse(@"account\login");
            }

            var success = users.Find(username, password);

            if (success)
            {
                this.LoginUser(req, username);

                return new RedirectResponse("/");
            }
            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid username or password.";
                this.ViewData["authDisplay"] = "none";

                return this.FileViewResponse(@"account\login");
            }
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = users.Profile(username);

            this.ViewData["username"] = profile.Username;
            this.ViewData["registrationDate"] = profile.RegistrationDate.ToLongDateString();
            this.ViewData["ordersCount"] = profile.OrdersCount.ToString();

            return FileViewResponse(@"account\profile");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/");
        }

        private void SetDefaultViewData()
        {
            this.ViewData["authDisplay"] = "none";
        }

        private void LoginUser(IHttpRequest req, string username)
        {
            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            this.ViewData["authDisplay"] = "block";
        }

    }
}