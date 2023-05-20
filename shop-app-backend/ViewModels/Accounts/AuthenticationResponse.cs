namespace ViewModels.Accounts
{
    public class AuthenticationResponse
    {
        public string Token { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
