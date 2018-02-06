namespace WebServer.ByTheCakeApplication.Controllers
{
    using Data;
    using Infrastructure;
    using System;
    using Server.Http.Contracts;
    using System.Linq;
    using ViewModels;

    public class CakesController : Controller
    {
       private readonly CakesData cakes = new CakesData();

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            this.cakes.Add(name, price);

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string SearchTermKey = "searchTerm";

            this.ViewData["results"] = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            var urlParameters = req.UrlParameters;

            if (urlParameters.ContainsKey(SearchTermKey))
            {
                var searchTerm = urlParameters[SearchTermKey];

                this.ViewData["searchTerm"] = searchTerm;


                var savedCakes = this.cakes
                    .All()
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .OrderBy(c => c.Name)
                    .Select(c => $@"<div>{c.Name} - ${c.Price:f2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>")
                    .ToList();

                var results = string.Join(Environment.NewLine, savedCakes);

                this.ViewData["results"] = results;

            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ?"products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";

            }

            return this.FileViewResponse(@"cakes\search");
        }
    }
}
