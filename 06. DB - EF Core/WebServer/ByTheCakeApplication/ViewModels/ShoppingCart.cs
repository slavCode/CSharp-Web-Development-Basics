namespace WebServer.ByTheCakeApplication.ViewModels
{
    using System.Collections.Generic;
        
    public class ShoppingCart
    { 
        public const string SessionKey = "%<Current_Shoping_Key>%";

        public IList<int> ProductIds { get; private set; } = new List<int>();
    }
}
