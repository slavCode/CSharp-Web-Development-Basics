namespace HandmadeHTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using System.Linq;
    using Models;
    using System;
    using System.Collections.Generic;
    using Server.Http.Contracts;
    using System.IO;

    public class CakesController : Controller
    {
        private const string DbFilePath = @"ByTheCakeApplication\Data\database.csv";

        private static readonly List<Cake> cakes = new List<Cake>();

        private string inputLine;

        private string resultsToShow;

        public IHttpResponse Add() => this.FileViewResponse(@"cakes\add", new Dictionary<string, string>
        {
            ["display"] = "none"
        });

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);

            if (new FileInfo(DbFilePath).Length == 0)
            {
                inputLine = cake.Name + "," + cake.Price;
            }

            else
            {
                inputLine = Environment.NewLine + cake.Name + "," + cake.Price;
            }

            File.AppendAllText(DbFilePath, inputLine);

            return this.FileViewResponse(@"cakes\add", new Dictionary<string, string>
            {
                ["name"] = name,
                ["price"] = price,
                ["display"] = "block"
            });
        }

        public IHttpResponse Search() => this.FileViewResponse(@"cakes\search");

        public IHttpResponse Search(IHttpRequest request)
        {
            var searchTermKey = "searchTerm";

            if (request.UrlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = request.UrlParameters[searchTermKey];
                var results = File.ReadAllLines(DbFilePath)
                    .Where(l => l.Contains(','))
                    .Select(l => l.Split(','))
                    .Select(l => new Cake
                    {
                        Name = l[0],
                        Price = decimal.Parse(l[1])
                    })
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $"<div>{c.Name} - ${c.Price}</div>");

                this.resultsToShow = string.Join(Environment.NewLine, results);
            }

            return this.FileViewResponse(@"cakes\search", new Dictionary<string, string>
            {
                ["results"] = this.resultsToShow
            });
        }

    }
}
