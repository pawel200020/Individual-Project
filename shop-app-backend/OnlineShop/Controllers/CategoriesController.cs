using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPortal.Entities;
using ShopPortal.Helpers;
using ViewModels.Pagination;
using ViewModels.Shop.Categories;

namespace ShopPortal.Controllers
{
    /// <summary>
    /// Controller for product categories management, all endpoints need you to be logged in
    /// </summary>
    [Route("api/categories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public CategoriesController(ILogger<CategoriesController> logger, ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get categories with pagination
        /// </summary>
        /// <param name="paginationViewModel"></param>
        /// <returns>page with derived numbers of categories</returns>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<List<CategoryViewModel>>> Get([FromQuery] PaginationViewModel paginationViewModel)
        {
            var test = HttpContext.User.Claims.ToArray();
            _logger.LogInformation("Getting all categories");
            var queryable = _context.Categories.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var categories = await queryable.OrderBy(x => x.Name).Paginate(paginationViewModel).ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }

        /// <summary>
        /// Get all categories without pagination
        /// </summary>
        /// <returns>list of all categories</returns>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryViewModel>>> Get()
        {
            _logger.LogInformation("Getting all categories");
            var categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }


        /// <summary>
        /// Get category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>found category or nothing when not exists</returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryViewModel>> Get(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();
            else
            {
                return _mapper.Map<CategoryViewModel>(category);
            }
        }

        /// <summary>
        /// Add a category to add into database
        /// </summary>
        /// <param name="categoryCretationViewModel"></param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryCreationViewModel categoryCretationViewModel)
        {
            var category = _mapper.Map<Category>(categoryCretationViewModel);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Edit a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryViewModel"></param>
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryCreationViewModel categoryViewModel)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();
            category = _mapper.Map(categoryViewModel, category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Remove a category
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exitsts = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!exitsts)
            {
                return NotFound();
            }

            _context.Remove(new Category() {Id = id});
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
