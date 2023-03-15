using System.ComponentModel.DataAnnotations;
using OnlineShop.Validation;

namespace OnlineShop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public List<OrdersProducts>? OrdersProducts { get; set; }
    }
}
