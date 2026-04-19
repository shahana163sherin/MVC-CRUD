using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Repositories
{
    public class ProductRepositories : IProductRepositories
    {

        private static List<Product> products = new List<Product>()
        {
            new Product{ProductId=1,ProductName="Laptop",Price=15000},
            new Product{ProductId=2,ProductName="Mobile",Price=5000},
            new Product{ProductId=3,ProductName="Tablet",Price=8000}
        };

        public Product? GetById(int Id)
        {
            return products.FirstOrDefault(p => p.ProductId == Id);
        }
        public void Add(Product prd)
        {
            prd.ProductId = products.Any() ? products.Max(p => p.ProductId) + 1 : 1;
            products.Add(prd);
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if(existing != null)
            {
                products.Remove(existing);
            }
        }

        public IEnumerable<Product> GetAllProducts() => products;
        

       

        public void Update(int Id, Product prd)
        {
            var existing = GetById(Id);
            if (existing != null)
            {
                existing.ProductName = prd.ProductName;
                existing.Price = prd.Price;
            }
        }
    }
}
