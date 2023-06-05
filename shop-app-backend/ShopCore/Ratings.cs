using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShopCore
{
    public class Ratings
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public Ratings(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task Vote(Rating rating, string? email)
        {
            if (email == null) 
                throw new ArgumentNullException(nameof(email));
            var userId = (await _userManager.FindByNameAsync(email)).Id;
            var currentRate = await _context.Rating.FirstOrDefaultAsync(x => x.ProductId == rating.ProductId && x.UserId == userId);

            if (currentRate == null)
            {
                var newRating = new Rating()
                {
                    ProductId = rating.ProductId,
                    Rate = rating.Rate,
                    UserId = userId,
                };
                _context.Add(newRating);
            }
            else
            {
                currentRate.Rate = rating.Rate;
            }
            await _context.SaveChangesAsync();
        }
    }
}
