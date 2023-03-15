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
#pragma warning disable CS8602 //never be null!
            var firstLetter = (value as string)[0].ToString();
#pragma warning restore CS8602
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("first letter should be uppercase");
            }
            return ValidationResult.Success;
        }
    }
}
