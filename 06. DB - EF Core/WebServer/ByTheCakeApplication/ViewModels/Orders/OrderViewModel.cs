namespace WebServer.ByTheCakeApplication.ViewModels.Orders  
{
    using System;

    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Sum { get; set; }
    }
}
