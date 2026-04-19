using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Repositories
{
    public interface IProductRepositories
    {
        //IEnumerable<Product> GetAllProducts();
        Task<IEnumerable<Product>> GetAllProductsAsync();
        //Product? GetById(int Id);
        Task<Product?> GetByIdAsync(int Id);
        //void Add(Product prd);
        Task AddAsync(Product prd);
        //void Update(int Id, Product prd);
        Task UpdateAsync(int Id, Product prd);
        //void Delete(int id);
        Task DeleteAsync(int Id);
    }
}
