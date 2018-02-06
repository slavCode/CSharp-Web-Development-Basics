namespace WebServer.ByTheCakeApplication.Data.Models
{
    public class OrderProduct
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int OrderId { get; set; }    

        public Order Order { get; set; }
    }
}
