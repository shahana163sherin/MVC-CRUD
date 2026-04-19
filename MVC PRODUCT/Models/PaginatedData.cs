namespace MVC_PRODUCT.Models
{
    public class PaginatedData<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalItems { get; set; }
    }
}
