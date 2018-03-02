namespace GameStoreApplication.Controllers
{
    using Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Server.Enums;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Views;


    public abstract class Controller
    {
        protected const string LoginPath = @"account\login";
        protected const string RegisterPath = @"account\register";
        protected const string HomePath = "/";
        private const string DefaultPath = @"Resources\{0}.html";
        private const string ContentPlaceholder = "{{{content}}}";

        private readonly IUserService users;

        protected Controller(IHttpRequest request)
        {
            this.Request = request;
            this.users = new UserService();
            this.Authentication = new Authentication(false, false);

            this.ViewData = new Dictionary<string, string>
            {
                ["showError"] = "none",
            };

            ApplyViewData();
        }

        protected IHttpRequest Request { get; private set; }

        protected IDictionary<string, string> ViewData { get; private set; }

        protected Authentication Authentication { get; private set; }

        protected IHttpResponse RedirectResponse(string route)
            => new RedirectResponse(route);

        public IHttpResponse FileViewResponse(string fileName)
        {
            var resultHtml = ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    resultHtml = resultHtml.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(resultHtml));
        }

        private static string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            return layoutHtml.Replace(ContentPlaceholder, fileHtml);
        }

        protected string ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        return result.ErrorMessage;
                    }
                }
            }

            return null;
        }

        private void ApplyViewData()
        {
            var authenticatedDisplay = "none";
            var anonymousDisplay = "flex";
            var adminDisplay = "none";

            var isAuthenticated = this.Request
                .Session
                .Contains(SessionStore.CurrentUserKey);

            if (isAuthenticated)
            {
                authenticatedDisplay = "flex";
                anonymousDisplay = "none";

                var email = this.Request
                    .Session
                    .Get<string>(SessionStore.CurrentUserKey);

                var isAdmin = this.users.IsAdmin(email);

                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["authenticatedDisplay"] = authenticatedDisplay;
            this.ViewData["anonymousDisplay"] = anonymousDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
            this.ViewData["colsView"] = "class=\"col-4 ml-auto mr-auto text-center\"";
        }
    }
}
