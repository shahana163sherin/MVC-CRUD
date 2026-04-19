using System.ComponentModel.DataAnnotations;

namespace MVC_PRODUCT.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage ="Price is Required")]
        [Range(50,500000,ErrorMessage="Price must be between 50 and 500000")]
        public decimal Price { get; set; }
    }
}
