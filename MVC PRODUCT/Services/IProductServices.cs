using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Services
{
    public interface IProductServices
    {
        //inmemory
        //ApiResponse<IEnumerable<Product>> GetAllProducts(string? search, string? sort, decimal? minPrice, decimal? maxPrice, int page, int pageSize);

        Task<ApiResponse<PaginatedData<Product>>> GetAllProductsAsync(string? search,string? sort,decimal? minPrice,decimal? maxPrice,int page,int pageSize);
        //ApiResponse<Product?> GetById(int Id);
        Task<ApiResponse<Product?>> GetByIdAsync(int Id);

        //ApiResponse<string> Add(Product prd);
        Task<ApiResponse<string>> AddAsync(Product prd);

        //ApiResponse<string> Update(int Id, Product prd);
        Task<ApiResponse<string>> UpdateAsync(int Id, Product prd);
        //ApiResponse<string> Delete(int id);
        Task<ApiResponse<string>> DeleteAsync(int Id);

    }
}
