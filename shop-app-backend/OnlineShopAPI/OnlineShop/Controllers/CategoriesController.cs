using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Entities;
using OnlineShop.Filters;

namespace OnlineShop.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController :ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(IRepository repository, ILogger<CategoriesController> logger)
        {
            _logger= logger?? throw new ArgumentNullException(nameof(logger));
            _repository = repository?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [HttpGet("list")]
        [ResponseCache(Duration = 60)]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<Category>>> Get()
        {
            _logger.LogInformation("Getting all categories");
            return await _repository.GetAllCategories();
        }

        [HttpGet("{Id:int}")]
        public ActionResult<Category> Get(int Id)
        {
            _logger.LogDebug("executing get method");
            var cat = _repository.getCategoryById(Id);
            if (cat is null)
            {
                _logger.LogWarning($"Getting id non existing ({Id})");
                return NotFound();
            }
            return cat;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            _repository.AddCategory(category);
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Category category)
        {

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
