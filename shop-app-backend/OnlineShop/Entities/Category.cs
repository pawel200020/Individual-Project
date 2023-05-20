using System.ComponentModel.DataAnnotations;
using ShopPortal.Validation;

namespace ShopPortal.Entities
{
	public class Category
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "This field with name {0} required")]
		[StringLength(50)]
		[FirstLetterUppercase]
		public string Name { get; set; } = null!;
    }
}
