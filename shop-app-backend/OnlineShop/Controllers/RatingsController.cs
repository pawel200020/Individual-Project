using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCore;
using ViewModels.Rating;

namespace ShopPortal.Controllers
{
    /// <summary>
    /// Controller responsible for ratings related with product
    /// </summary>
    [ApiController]
    [Route("api/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly Ratings _ratings;
        private readonly IMapper _mapper;
        private readonly ILogger<RatingsController> _logger;

        /// <inheritdoc />
        public RatingsController(Ratings ratings, IMapper mapper, ILogger<RatingsController> logger)
        {
            _ratings = ratings ?? throw new ArgumentNullException(nameof(ratings));
            _mapper = mapper ?? throw  new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Insert Rating
        /// </summary>
        /// <param name="rating"></param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingViewModel rating)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            try
            {
                await _ratings.Vote(_mapper.Map<Rating>(rating), email);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }

            return NoContent();
        }
    }
}
