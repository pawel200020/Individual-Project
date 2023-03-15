using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Entities
{
    public class ProductsCategories
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
