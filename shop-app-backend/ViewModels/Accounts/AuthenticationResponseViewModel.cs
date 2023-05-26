namespace ViewModels.Accounts
{
    public class AuthenticationResponseViewModel
    {
        public string Token { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
