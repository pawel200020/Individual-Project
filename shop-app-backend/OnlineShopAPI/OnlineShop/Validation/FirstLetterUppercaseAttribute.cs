using System.ComponentModel.DataAnnotations;
using OnlineShop.Entities;

namespace OnlineShop.Validation
{
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            var firstLetter = (value as string)[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("first letter should be uppercase");
            }
            return ValidationResult.Success;
        }
    }
}
