using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Validation;

namespace ViewModels.Shop.Orders
{
    public class OrderCreationViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<OrdersProductsCreationViewModel>>))]
        [Required(ErrorMessage = "Order cannot be empty")]
        public List<OrdersProductsCreationViewModel>? OrdersProducts { get; set; }
    }
}
