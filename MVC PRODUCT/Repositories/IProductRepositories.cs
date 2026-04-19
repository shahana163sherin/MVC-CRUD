using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Repositories
{
    public interface IProductRepositories
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetById(int Id);
        void Add(Product prd);
        void Update(int Id, Product prd);
        void Delete(int id);
    }
}
