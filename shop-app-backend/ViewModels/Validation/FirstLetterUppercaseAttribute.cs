using System.ComponentModel.DataAnnotations;

namespace ViewModels.Validation
{
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            var firstLetter = (value as string)?[0].ToString();
            return firstLetter != firstLetter?.ToUpper() 
                ? new ValidationResult("first letter should be uppercase") 
                : ValidationResult.Success;
        }
    }
}
