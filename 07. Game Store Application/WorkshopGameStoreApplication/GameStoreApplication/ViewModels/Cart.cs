namespace GameStoreApplication.ViewModels
{
    using System.Collections.Generic;

    public class Cart
    {
        public const string SessionKey = "%<Current_Shoping_Key>%";

        public IList<int> ProductIds { get; private set; } = new List<int>();
    }
}
