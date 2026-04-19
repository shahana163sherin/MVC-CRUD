using MVC_PRODUCT.Models;
using MVC_PRODUCT.Repositories;

namespace MVC_PRODUCT.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepositories _repo;
        public ProductServices(IProductRepositories repo)
        {
            _repo = repo;
        }
        public ApiResponse<string> Add(Product prd)
        {
           
           if(prd == null)
            {
                return ApiResponse<string>.Fail(new List<string> { "Product cannot be null" }, "Validation Failed");
            }
            else
            {
                _repo.Add(prd);
                return ApiResponse<string>.Success("Product added successfully");
            }
        }

        public ApiResponse<string> Delete(int id)
        {
            if(id <= 0)
            {
                return ApiResponse<string>.Fail(new List<string> { "Invalid Product Id" }, "Validation Failed");
            }
            _repo.Delete(id);
            return ApiResponse<string>.Success("Product deleted successfully");
        }

        public ApiResponse<IEnumerable<Product>> GetAllProducts(string? search, string? sort, decimal? minPrice, decimal? maxPrice, int page, int pageSize)

        {
            var result= _repo.GetAllProducts();
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(r => r.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                result=result.Where(r=>r.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                result=result.Where(r=>r.Price <= maxPrice.Value);
            }
            result = sort switch
            {
                "name_desc" => result.OrderByDescending(r => r.ProductName),
                "price_asc" => result.OrderBy(r => r.Price),
                "price_desc" => result.OrderByDescending(r => r.Price),
                _=> result.OrderBy(r => r.ProductName)
            };

            var totalItems = result.Count();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return ApiResponse<IEnumerable<Product>>.Success(pagedData);
        }

        public ApiResponse<Product?> GetById(int Id)
        {
            var result = _repo.GetById(Id);
            if(result == null)
            {
                return ApiResponse<Product?>.Fail(new List<string> { "Product not found" }, "Not Found");
            }

            return ApiResponse<Product?>.Success(result);
        }

        public ApiResponse<string> Update(int Id, Product prd)
        {
            if(prd == null)
            {
                return ApiResponse<string>.Fail(new List<string> { "Product cannot be null" }, "Validation Failed");
            }
            if(Id <= 0)
            {
                return ApiResponse<string>.Fail(new List<string> { "Invalid Product Id" }, "Validation Failed");
            }

            _repo.Update(Id, prd);
            return ApiResponse<string>.Success("Product updated successfully");
        }
    }
}
