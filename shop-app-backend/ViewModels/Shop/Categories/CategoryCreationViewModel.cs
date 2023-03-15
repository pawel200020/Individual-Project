using System.ComponentModel.DataAnnotations;
using ViewModels.Validation;

namespace ViewModels.Shop.Categories
{
    public class CategoryCreationViewModel
    {

        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
    }
}
