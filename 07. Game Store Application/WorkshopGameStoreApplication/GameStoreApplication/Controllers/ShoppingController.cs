namespace GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using ViewModels;

    public class ShoppingController : Controller
    {
        public ShoppingController(IHttpRequest request)
            : base(request)
        {
        }

        public IHttpResponse AddToCart()
        {
            var id = int.Parse(this.Request.UrlParameters["id"]);

            var cart = this.Request.Session.Get<Cart>(Cart.SessionKey);

            cart.ProductIds.Add(id);


            return null;
        }
    }
}
