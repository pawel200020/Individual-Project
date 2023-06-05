using System.ComponentModel.DataAnnotations;

namespace ViewModels.Shop.Orders
{
    public class OrdersProductsCreationViewModel
    {
        public int Id { get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = "Product Quantity must be bigger than 0!")]
        public int Quantity { get; set; }
    }
}
