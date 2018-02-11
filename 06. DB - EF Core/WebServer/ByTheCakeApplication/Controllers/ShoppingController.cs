namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using ViewModels;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using System;
    using System.Linq;

    public class ShoppingController : Controller
    {
        private readonly IProductService products = new ProductService();
        private readonly IUserService users = new UserService();
        private readonly IShoppingService shopping = new ShoppingService();

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var productExists = products.Exists(id);

            if (!productExists)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            shoppingCart.ProductIds.Add(id);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.ProductIds.Any())
            {
                this.ViewData["cartItems"] = " No items in your cart.";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var productsInCart = this.products
                    .FindProductsInCart(shoppingCart.ProductIds);

                var items = productsInCart
                    .Select(p => $"<div>{p.Name} - ${p.Price:F2}</div><br />");

                this.ViewData["cartItems"] = string.Join(string.Empty, items);

                var totalCost = productsInCart
                    .Sum(c => c.Price);

                this.ViewData["totalCost"] = $"{totalCost:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.users.GetUserId(username);
            if (userId == null)
            {
                throw new InvalidOperationException("Username cannot be found.");
            }

            var productIds = shoppingCart.ProductIds;
            if (!productIds.Any())
            {
                return new RedirectResponse("/");
            }

            this.shopping.CreateOrder(userId, productIds);

            shoppingCart.ProductIds.Clear();

            return this.FileViewResponse(@"Shopping\finish-order");
        }
    }
}
