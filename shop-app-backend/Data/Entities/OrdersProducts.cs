using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class OrdersProducts
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
