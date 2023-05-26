using ViewModels.Accounts;

namespace ShopPortal.Helpers
{
    public class AuthenticationResponseResult
    {
        public AuthenticationResponse? AuthenticationResponse { get; set; }
        public object? Errors { get; set; }
    }
}
