using Microsoft.AspNetCore.Mvc;
using ShopPortal.Security;
using ViewModels.Accounts;

namespace ShopPortal.Controllers
{
    /// <summary>
    /// sample comment
    /// </summary>
    [ApiController]
    [Route("api/accounts")]

    public class AccountController : ControllerBase
    {
        private readonly IAccounts _accounts;

        /// <inheritdoc />
        public AccountController(IAccounts accounts)
        {
            _accounts = accounts ?? throw  new ArgumentNullException(nameof(accounts));
        }

        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>User token valid 1 day</returns>
        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCredentials userCredentials)
        {
            var result = await _accounts.Register(userCredentials);

            if (result.Errors is null && result.AuthenticationResponse != null)
                return result.AuthenticationResponse;

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login to an existing account
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>User token valid 1 day</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredentials)
        {
            var result =await _accounts.Login(userCredentials);

            if (result.Errors is null && result.AuthenticationResponse != null)
                return result.AuthenticationResponse;

            return BadRequest(result.Errors);
        }
    }
}
