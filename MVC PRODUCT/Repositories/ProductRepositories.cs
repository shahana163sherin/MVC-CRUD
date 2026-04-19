using Microsoft.EntityFrameworkCore;
using MVC_PRODUCT.Data;
using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly DBContext _context;
        public ProductRepositories(DBContext context)
        {
            _context = context;
        }

        //private static List<Product> products = new List<Product>()
        //{
        //    new Product{ProductId=1,ProductName="Laptop",Price=15000},
        //    new Product{ProductId=2,ProductName="Mobile",Price=5000},
        //    new Product{ProductId=3,ProductName="Tablet",Price=8000}
        //};

        //inmemory data     
        //public Product? GetById(int Id)
        //{
        //    return products.FirstOrDefault(p => p.ProductId == Id);
        //}

        public async Task< Product?> GetByIdAsync(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }
        //public void Add(Product prd)
        //{
        //    prd.ProductId = products.Any() ? products.Max(p => p.ProductId) + 1 : 1;
        //    products.Add(prd);
        //}

        public async Task AddAsync(Product prd)
        {
            //prd.ProductId = await _context.Products.AnyAsync() ? await _context.Products.MaxAsync(p => p.ProductId) + 1 : 1;
            await _context.Products.AddAsync(prd);
            await _context.SaveChangesAsync();
        }

        //public void Delete(int id)
        //{
        //    var existing = GetById(id);
        //    if(existing != null)
        //    {
        //        products.Remove(existing);
        //    }
        //}


        public async Task DeleteAsync(int Id)
        {
            var existing = await GetByIdAsync(Id);
            if(existing != null)
            {
                _context.Products.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
        //In-Memory data
        //public IEnumerable<Product> GetAllProducts() => products;
        public async Task <IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        

       

        //public void Update(int Id, Product prd)
        //{
        //    var existing = GetById(Id);
        //    if (existing != null)
        //    {
        //        existing.ProductName = prd.ProductName;
        //        existing.Price = prd.Price;
        //    }
        //}

        public async Task UpdateAsync(int Id,Product prd)
        {
            var existing =  await GetByIdAsync(Id);
            if(existing != null)
            {
                existing.ProductName = prd.ProductName;
                existing.Price = prd.Price;
                await _context.SaveChangesAsync();
            }
        }
    }
}
