using ShopPortal.Helpers;
using ViewModels.Accounts;

namespace ShopPortal.Security;

public interface IAccounts
{
    Task<AuthenticationResponseResult> Register(UserCredentials userCredentials);
    Task<AuthenticationResponseResult> Login(UserCredentials userCredentials);
}