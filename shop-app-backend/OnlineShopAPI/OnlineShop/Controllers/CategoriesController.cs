using Microsoft.AspNetCore.Mvc;
using OnlineShop.Entities;

namespace OnlineShop.Controllers
{
    [Route("api/categories")]
    public class CategoriesController :ControllerBase
    {
        private readonly IRepository _repository;
        public CategoriesController(IRepository repository)
        {
            _repository = repository?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [HttpGet("list")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await _repository.GetAllCategories();
        }

        [HttpGet("{Id:int}")]
        public ActionResult<Category> Get(int Id)
        {
            var cat = _repository.getCategoryById(Id);
            if (cat is null)
            {
                return NotFound();
            }
            return cat;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put()
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
