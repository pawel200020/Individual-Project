using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Entities;
using OnlineShop.Helpers;
using OnlineShop.Validation;

namespace OnlineShop.DTO
{
    public class OrderCreationDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; }
        public double Value { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<OrdersProductsCreationDTO>>))]
        public List<OrdersProductsCreationDTO> OrdersProducts { get; set; }
    }
}
