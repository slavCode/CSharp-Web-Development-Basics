namespace HandmadeHTTPServer.ByTheCakeApplication.Infrastructure
{
    using System.Collections.Generic;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.IO;
    using Views;

    public abstract class Controller
    {
        private const string DefaultPath = @"ByTheCakeApplication\Resources\{0}.html";
        private const string ContentPlaceholder = "{{{content}}}";

        public IHttpResponse FileViewResponse(string fileName)
        {
            var resultHtml = ProcessFileHtml(fileName);

            return new ViewResponse(HttpStatusCode.Ok, new FileView(resultHtml));
        }

        public IHttpResponse FileViewResponse(string fileName, Dictionary<string, string> values)
        {
            var resultHtml = ProcessFileHtml(fileName);

            foreach (var value in values)
            {
                resultHtml = resultHtml.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(resultHtml));
        }

        private static string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            return layoutHtml.Replace(ContentPlaceholder, fileHtml);
        }
    }
}
