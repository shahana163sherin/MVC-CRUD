using Microsoft.EntityFrameworkCore;
using MVC_PRODUCT.Models;

namespace MVC_PRODUCT.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
