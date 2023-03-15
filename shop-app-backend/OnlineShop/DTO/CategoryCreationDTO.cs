using System.ComponentModel.DataAnnotations;
using OnlineShop.Validation;

namespace OnlineShop.DTO
{
    public class CategoryCreationDTO
    {
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; }
    }
}
