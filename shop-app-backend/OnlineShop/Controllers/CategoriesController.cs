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
    [Route("api/categories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger, ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

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

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryViewModel>>> Get()
        {
            _logger.LogInformation("Getting all categories");
            var categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }


        [HttpGet("{Id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryViewModel>> Get(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null)
                return NotFound();
            else
            {
                return _mapper.Map<CategoryViewModel>(category);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryCreationViewModel categoryCretationViewModel)
        {
            var category = _mapper.Map<Category>(categoryCretationViewModel);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

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
