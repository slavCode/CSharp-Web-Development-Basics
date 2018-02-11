namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using System;
    using System.Linq;
    using Services;
    using Server.Http;
    using Server.Http.Contracts;

    public class OrdersController : Controller
    {
        private readonly IOrderService orders = new OrderService();

        public IHttpResponse Orders(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var result = orders.FindOrders(username).OrderByDescending(o => o.CreatedOn);

            var resultTable = result
                .Select(p => $@"<tr><th>{p.Id}</th><th>{p.CreatedOn}</th><th>{p.Sum}</th></tr>");

            this.ViewData["ordersList"] = string.Join(Environment.NewLine, resultTable);

            return this.FileViewResponse(@"orders\orders");
        }

        //public IHttpResponse OrderDetails(IHttpRequest req)
        //{
        //    var id = int.Parse(req.UrlParameters["id"]);

        //    var order = orders.ShowProducts(id);
            
        //    var products = order
        //        .Select(p => $@"<tr><th>{p.Name}</th><th>{p.Price}</th></tr>");

        //    this.ViewData["orderId"] = id.ToString();
        //    this.ViewData["products"] = string.Join(Environment.NewLine, products);

        //    return FileViewResponse(@"orders\orderDetails");
        //}
    }
}
