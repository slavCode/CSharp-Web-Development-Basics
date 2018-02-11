namespace WebServer.ByTheCakeApplication.Services
{
    using System.Collections.Generic;
    using ViewModels.Orders;

    public interface IOrderService
    {
        IEnumerable<OrderViewModel> FindOrders(string username);


        // IEnumerable<ProductInCartViewModel> ShowProducts(int orderId);
    }
}
