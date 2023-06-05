using AutoMapper;
using Data.Entities;
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
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public AccountController(IAccounts accounts, IMapper mapper)
        {
            _accounts = accounts ?? throw  new ArgumentNullException(nameof(accounts));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="userCredentialsViewModel"></param>
        /// <returns>User token valid 1 day</returns>
        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponseViewModel>> Create([FromBody] UserCredentialsViewModel userCredentialsViewModel)
        {
            var result = await _accounts.Register(_mapper.Map<UserCredentials>(userCredentialsViewModel));

            if (result.Errors is null && result.AuthenticationResponse != null)
                return _mapper.Map<AuthenticationResponseViewModel>(result.AuthenticationResponse);

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login to an existing account
        /// </summary>
        /// <param name="userCredentialsViewModel"></param>
        /// <returns>User token valid 1 day</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseViewModel>> Login([FromBody] UserCredentialsViewModel userCredentialsViewModel)
        {
            var result =await _accounts.Login(_mapper.Map<UserCredentials>(userCredentialsViewModel));

            if (result.Errors is null && result.AuthenticationResponse != null)
                return _mapper.Map<AuthenticationResponseViewModel>(result.AuthenticationResponse);

            return BadRequest(result.Errors);
        }
    }
}
