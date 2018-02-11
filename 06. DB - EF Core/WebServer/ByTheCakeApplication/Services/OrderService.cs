namespace WebServer.ByTheCakeApplication.Services
{
    using Data;
    using System.Linq;
    using System.Collections.Generic;
    using ViewModels.Orders;
    using ViewModels.Products;
    
    public class OrderService : IOrderService
    {
        public IEnumerable<OrderViewModel> FindOrders(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var result = db
                    .Orders
                    .Where(o => o.User.Username == username)
                    .Select(u => new OrderViewModel
                    {
                        CreatedOn = u.CreationDate,
                        Sum = u.Products.Sum(p => p.Product.Price),
                        Id = u.Id
                    })
                    .ToList();

                return result;
            }
        }

        // TODO 
        // Show order products
        //public IEnumerable<ProductInCartViewModel> ShowProducts(int orderId)
        //{
        //    using (var db = new ByTheCakeDbContext())
        //    {
        //        return null;
        //    }
        //}
    }
}
