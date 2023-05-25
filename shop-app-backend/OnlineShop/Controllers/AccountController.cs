using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        /// <inheritdoc />
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>User token valid 1 day</returns>
        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email};
            var result = await _userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
                return await BuildToken(userCredentials);

            return BadRequest(result.Errors.Select(x=>x.Description));
        }

        /// <summary>
        /// Login to an exisiting account
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>User token valid 1 day</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredentials)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email,userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
                return  await BuildToken(userCredentials);

            return BadRequest(new[]{"incorrect login or password"});
        }

        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new ("email", userCredentials.Email)
            };
            var user = await _userManager.FindByNameAsync(userCredentials.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["keyjwt"] ?? throw new InvalidOperationException()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var expiration = DateTime.UtcNow.AddDays(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration,
                signingCredentials: creds);
            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expiration
            };
        }
    }

}
