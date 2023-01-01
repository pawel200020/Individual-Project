using System.ComponentModel.DataAnnotations;
using OnlineShop.Validation;

namespace OnlineShop.Entities
{
    public class Category : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(10)]
        [FirstLetterUppercase]
        public string Name { get; set; }
        //[Range(18,25)]
        //public int Age { get; set; }
        //[CreditCard]
        //public string CreditCard { get; set; }
        //[Url]
        //public string Url { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!string.IsNullOrEmpty(Name))
            {
                var firstLetter = Name[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("first letter should be uppercase", new string[] {nameof(Name)});
                }

            }
        }
    }
}
