using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Services
{
    public interface IProductServices
    {
        ApiResponse<IEnumerable<Product>> GetAllProducts(string? search,string? sort,decimal? minPrice,decimal? maxPrice,int page,int pageSize);
        ApiResponse<Product?> GetById(int Id);
        ApiResponse<string> Add(Product prd);
        ApiResponse<string> Update(int Id, Product prd);
        ApiResponse<string> Delete(int id);

    }
}
