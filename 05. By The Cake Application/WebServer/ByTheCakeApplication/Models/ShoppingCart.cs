namespace WebServer.ByTheCakeApplication.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    { 
        public const string SessionKey = "%<Current_Shoping_Key>%";

        public IList<Cake> Orders { get; private set; } = new List<Cake>();
    }
}
