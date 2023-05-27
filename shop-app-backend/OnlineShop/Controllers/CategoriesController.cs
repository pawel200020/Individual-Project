using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCore;
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
    [Authorize(Policy = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Categories _categories;
        private readonly ILogger<CategoriesController> _logger;
        /// <inheritdoc />
        public CategoriesController(IMapper mapper, Categories categories, ILogger<CategoriesController> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categories = categories ?? throw new ArgumentNullException(nameof(categories));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get categories with pagination
        /// </summary>
        /// <param name="paginationViewModel"></param>
        /// <returns>page with derived numbers of categories</returns>
        [HttpGet]
        public async Task<ActionResult<CategoryViewModel[]>> Get([FromQuery] PaginationViewModel paginationViewModel)
        {
            var (categories, quantity) = await _categories.GetAllCategoriesPaged(_mapper.Map<PaginationModel>(paginationViewModel));
            HttpContext.InsertParametersPaginationInHeader(quantity);
            return _mapper.Map<CategoryViewModel[]>(categories);
        }

        /// <summary>
        /// Get all categories without pagination
        /// </summary>
        /// <returns>list of all categories</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<CategoryViewModel>>> Get()
        {
            return _mapper.Map<List<CategoryViewModel>>(await _categories.GetAllCategories());
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
            var category = await _categories.GetById(id);
            if (category is null)
                return NotFound();

            return _mapper.Map<CategoryViewModel>(category);
        }

        /// <summary>
        /// Add a category to add into database
        /// </summary>
        /// <param name="categoryCreationViewModel"></param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryCreationViewModel categoryCreationViewModel)
        {
            await _categories.Create(_mapper.Map<Category>(categoryCreationViewModel));
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
            try
            {
               await _categories.Edit(id, _mapper.Map<Category>(categoryViewModel));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Remove a category
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categories.Delete(id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }
            return NoContent();
        }
    }
}
