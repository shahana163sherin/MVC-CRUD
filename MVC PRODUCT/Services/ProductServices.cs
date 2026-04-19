using MVC_PRODUCT.Models;
using MVC_PRODUCT.Repositories;
using System.Threading.Tasks;

namespace MVC_PRODUCT.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepositories _repo;
        public ProductServices(IProductRepositories repo)
        {
            _repo = repo;
        }
        //public ApiResponse<string> Add(Product prd)
        //{

        //   if(prd == null)
        //    {
        //        return ApiResponse<string>.Fail(new List<string> { "Product cannot be null" }, "Validation Failed");
        //    }
        //    else
        //    {
        //        _repo.Add(prd);
        //        return ApiResponse<string>.Success("Product added successfully");
        //    }
        //}

        public async Task<ApiResponse<string>> AddAsync(Product prd)
        {
            if (string.IsNullOrEmpty(prd.ProductName))
            {
                return ApiResponse<string>.Fail(new List<string> { "Product Name cannot be empty" }, "Validation Failed");
            }
            if (prd == null)
            {
                return ApiResponse<string>.Fail(new List<string> { "Product cannot be null" }, "Validation Failed");
            }
           await _repo.AddAsync(prd);
            return ApiResponse<string>.Success( "Product added successfully");
        }

        //public ApiResponse<string> Delete(int id)
        //{
        //    if(id <= 0)
        //    {
        //        return ApiResponse<string>.Fail(new List<string> { "Invalid Product Id" }, "Validation Failed");
        //    }
        //    _repo.Delete(id);
        //    return ApiResponse<string>.Success("Product deleted successfully");
        //}

        public async Task<ApiResponse<string>>DeleteAsync(int Id)
        {
            if (Id <= 0)
            {
                return ApiResponse<string>.Fail(new List<string> { "Invalid Product Id" }, "Validation Failed");
            }
            await _repo.DeleteAsync(Id);
            return ApiResponse<string>.Success("Product deleted successfully");
        }

        //public ApiResponse<IEnumerable<Product>> GetAllProducts(string? search, string? sort, decimal? minPrice, decimal? maxPrice, int page, int pageSize)

        //{
        //    var result= _repo.GetAllProducts();
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        result = result.Where(r => r.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase));
        //    }

        //    if (minPrice.HasValue)
        //    {
        //        result=result.Where(r=>r.Price >= minPrice.Value);
        //    }

        //    if (maxPrice.HasValue)
        //    {
        //        result=result.Where(r=>r.Price <= maxPrice.Value);
        //    }
        //    result = sort switch
        //    {
        //        "name_desc" => result.OrderByDescending(r => r.ProductName),
        //        "price_asc" => result.OrderBy(r => r.Price),
        //        "price_desc" => result.OrderByDescending(r => r.Price),
        //        _=> result.OrderBy(r => r.ProductName)
        //    };

        //    var totalItems = result.Count();
        //    var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //    return ApiResponse<IEnumerable<Product>>.Success(pagedData);
        //}

        public async Task<ApiResponse<PaginatedData<Product>>>GetAllProductsAsync(string? search, string? sort, decimal? minPrice, decimal? maxPrice , int page ,  int pageSize)
        {
            var result = await _repo.GetAllProductsAsync();
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(r => r.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            if (minPrice.HasValue){
                result = result.Where(r => r.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                result = result.Where(r => r.Price <= maxPrice.Value);
            }

            result = sort switch
            {
                "name_desc" => result.OrderByDescending(r => r.ProductName),
                "name_asc" => result.OrderBy(r => r.ProductName),
                "price_asc" => result.OrderBy(r => r.Price),
                "price_desc" => result.OrderByDescending(r => r.Price),
                _ => result.OrderBy(r => r.ProductId)
            };
            var totalItems = result.Count();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new ApiResponse<PaginatedData<Product>>
            {
                IsSuccess = true,
                Data = new PaginatedData<Product>
                {
                    Data = pagedData,
                    TotalItems = totalItems
                }
            };

        }

        //public ApiResponse<Product?> GetById(int Id)
        //{
        //    var result = _repo.GetById(Id);
        //    if(result == null)
        //    {
        //        return ApiResponse<Product?>.Fail(new List<string> { "Product not found" }, "Not Found");
        //    }

        //    return ApiResponse<Product?>.Success(result);
        //}

        public async Task<ApiResponse<Product?>>GetByIdAsync(int Id)
        {
            var result = await _repo.GetByIdAsync(Id);
            if (result == null)
            {
                return ApiResponse<Product?>.Fail(new List<string> { "Product not found" }, "Not Found");
            }
            return ApiResponse<Product?>.Success(result);
        }

        //public ApiResponse<string> Update(int Id, Product prd)
        //{
        //    if(prd == null)
        //    {
        //        return ApiResponse<string>.Fail(new List<string> { "Product cannot be null" }, "Validation Failed");
        //    }
        //    if(Id <= 0)
        //    {
        //        return ApiResponse<string>.Fail(new List<string> { "Invalid Product Id" }, "Validation Failed");
        //    }

        //    _repo.Update(Id, prd);
        //    return ApiResponse<string>.Success("Product updated successfully");
        //}

        public async Task<ApiResponse<string>>UpdateAsync(int Id,Product prd)
        {
                       if (prd == null)
            {
                return ApiResponse<string>.Fail(new List<string> { "Product cannot be null" }, "Validation Failed");
            }
            if (Id <= 0)
            {
                return ApiResponse<string>.Fail(new List<string> { "Invalid Product Id" }, "Validation Failed");
            }
            await _repo.UpdateAsync(Id, prd);
            return ApiResponse<string>.Success("Product updated successfully");
        }
    }
}
