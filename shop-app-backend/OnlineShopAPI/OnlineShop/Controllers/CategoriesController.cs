using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTO;
using OnlineShop.Entities;
using OnlineShop.Filters;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers
{
    [Route("api/categories")]
    [ApiController]
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
        //      [HttpGet("list")]
        //      [ResponseCache(Duration = 60)]
        public async Task<ActionResult<List<CategoryDTO>>> Get([FromQuery] PaginationDTO paginationDto)
        {
            _logger.LogInformation("Getting all categories");
            var queryable = _context.Categories.AsQueryable();
            await HttpContext.InsertParamtersPanginationInHeader(queryable);
            var categories = await queryable.OrderBy(x => x.Name).Paginate(paginationDto).ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CategoryDTO>> Get(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null)
                return NotFound();
            else
            {
                return _mapper.Map<CategoryDTO>(category);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryCreationDTO categoryCretationDTO)
        {
            var category = _mapper.Map<Category>(categoryCretationDTO);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryCreationDTO categoryDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();
            category = _mapper.Map(categoryDto, category);
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
