using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShopPortal.Helpers;
using ViewModels.Accounts;
using Data.Entities;

namespace ShopPortal.Security
{
    public class Accounts : IAccounts
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public Accounts(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<AuthenticationResponseResult> Register(UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var result =  await _userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
                return new AuthenticationResponseResult() {AuthenticationResponse = await BuildToken(userCredentials)};

            return new AuthenticationResponseResult() {Errors = result.Errors.Select(x => x.Description)};
        }

        public async Task<AuthenticationResponseResult> Login(UserCredentials userCredentials)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
                return new AuthenticationResponseResult(){AuthenticationResponse = await BuildToken(userCredentials)};

            return new AuthenticationResponseResult(){Errors = new[]{"Incorrect user login or password, please try again."}};
        }

        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentialsViewModel)
        {
            var claims = new List<Claim>()
            {
                new ("email", userCredentialsViewModel.Email)
            };
            var user = await _userManager.FindByNameAsync(userCredentialsViewModel.Email);
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
