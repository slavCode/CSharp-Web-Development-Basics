using WebServer.ByTheCakeApplication.Data.Models;

namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Server.Http;
    using Services;
    using System;
    using System.Linq;
    using ViewModels;
    using ViewModels.Products;

    public class ProductsController : Controller
    {
        private const string AddView = @"products\add";

        private readonly ProductService products = new ProductService();

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(AddView);
        }

        public IHttpResponse Add(IHttpRequest req, ProductViewModel product)
        {
            this.products.Add(product);

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString();
            this.ViewData["pictureUrl"] = product.PictureUrl;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(AddView);
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string SearchTermKey = "searchTerm";

            this.ViewData["results"] = string.Empty;

            var urlParameters = req.UrlParameters;

            var searchTerm = urlParameters.ContainsKey(SearchTermKey)
                ? urlParameters[SearchTermKey]
                : null;

            this.ViewData["searchTerm"] = searchTerm;

            var result = this.products.All(searchTerm);

            if (!result.Any())
            {
                this.ViewData["results"] = "No cakes found.";
            }
            else
            {
                var allProducts = result
                    .Select(c => $@"<div><a href=""/cakes/{c.Id}"">{c.Name}</a> - ${c.Price:f2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>")
                    .ToList();

                this.ViewData["results"] = string.Join(Environment.NewLine, allProducts);
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.ProductIds.Any())
            {
                var totalProducts = shoppingCart.ProductIds.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";

            }

            return this.FileViewResponse(@"products\search");
        }

        public IHttpResponse Details(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var result = this.products.Find(id);

            if (result == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["name"] = result.Name;
            this.ViewData["price"] = result.Price.ToString("F2");
            this.ViewData["imageUrl"] = result.ImageUrl;

            return FileViewResponse(@"products\details");
        }
    }
}
