using System.ComponentModel.DataAnnotations;

namespace ViewModels.Accounts
{
    public class UserCredentials
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
