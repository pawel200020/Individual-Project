using System.ComponentModel.DataAnnotations;
using OnlineShop.Validation;

namespace OnlineShop.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; }
        public bool IsAvalible { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public double Price { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        public DateTime ManufactureDate { get; set; }
        public string Picture { get; set; }
        public string Caption { get; set; }
        public List<ProductsCategories> ProductsCategories { get; set; }
    }
}
