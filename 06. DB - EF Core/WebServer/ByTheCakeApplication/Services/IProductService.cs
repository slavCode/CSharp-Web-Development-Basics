namespace WebServer.ByTheCakeApplication.Services
{
    using System.Collections.Generic;
    using ViewModels.Products;

    public interface IProductService
    {
        void Add(ProductViewModel model);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null);

        ProductDetailsViewModel Find(int id);

        bool Exists(int id);

        IEnumerable<ProductInCartViewModel> FindProductsInCart(IEnumerable<int> ids);
    }
}
