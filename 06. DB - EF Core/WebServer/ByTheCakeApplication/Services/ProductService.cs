namespace WebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Products;

    public class ProductService : IProductService
    {
        public void Add(ProductViewModel model)
        {
            using (var db = new ByTheCakeDbContext())
            {
                db.Products.Add(new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    ImageUrl = model.PictureUrl
                });

                db.SaveChanges();
            }
        }

        public IEnumerable<ProductListingViewModel> All(string searchTerm = null)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var resultQuery = db.Products.AsQueryable();


                if (!string.IsNullOrEmpty(searchTerm))
                {
                    resultQuery = resultQuery
                        .Where(p => p.Name.ToLower()
                        .Contains(searchTerm.ToLower()));
                }

                return resultQuery
                    .Select(pr => new ProductListingViewModel
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        Price = pr.Price
                    })
                    .ToList();
            }
        }

        public ProductDetailsViewModel Find(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var resultProduct = db
                    .Products
                    .FirstOrDefault(p => p.Id == id);

                if (resultProduct != null)
                {
                    return new ProductDetailsViewModel
                    {
                        Name = resultProduct.Name,
                        ImageUrl = resultProduct.ImageUrl,
                        Price = resultProduct.Price
                    };
                }

                return null;
            }
        }

        public bool Exists(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products.Any(p => p.Id == id);
            }
        }

        public IEnumerable<ProductInCartViewModel> FindProductsInCart(IEnumerable<int> ids)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db
                    .Products
                    .Where(p => ids.Contains(p.Id))
                    .Select(p => new ProductInCartViewModel
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList();
            }
        }
    }
}
