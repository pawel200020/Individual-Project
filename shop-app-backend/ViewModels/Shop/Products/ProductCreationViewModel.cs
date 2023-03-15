using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Validation;

namespace ViewModels.Shop.Products
{
    public class ProductCreationViewModel
    {
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
        public bool IsAvalible { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public double Price { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public IFormFile? Picture { get; set; }
        public string? Caption { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int>? CategoriesIds { get; set; }
    }
}
