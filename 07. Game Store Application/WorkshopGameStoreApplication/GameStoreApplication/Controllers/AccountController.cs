namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using Server.Http;
    using Services;
    using ViewModels.Account;

    public class AccountController : Controller
    {
       private readonly UserService users;

        public AccountController(IHttpRequest request)
            : base(request)
        {
            this.users = new UserService();
        }

        public IHttpResponse Register()
        {
            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse Register(RegisterUserViewModel model)
        {
            var error = this.ValidateModel(model);
            if (error != null)
            {
                ShowError(error);

                return this.FileViewResponse(RegisterPath);
            }

            var success = this.users.Create(model);

            if (success)
            {
                LoginUser(model.Email);

                return this.FileViewResponse(LoginPath);
            }

            ShowError("This e-mail is taken.");

            return this.FileViewResponse(RegisterPath);
        }

        public IHttpResponse Login()
        {
            return this.FileViewResponse(LoginPath);
        }

        public IHttpResponse Login(LoginUserViewModel model)
        {
            var success = this.users.Find(model);

            if (success)
            {
                LoginUser(model.Email);

                return this.RedirectResponse(HomePath);
            }

            ShowError("Invalid name or password.");

            return this.Login();
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return this.RedirectResponse(HomePath);
        }

        private void ShowError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        private void LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
        }
    }
}
