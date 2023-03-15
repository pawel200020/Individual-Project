using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public class InMemoryRepository : IRepository
    {
        private readonly ILogger<InMemoryRepository> _logger;
        private List<Category> _categories;
        public InMemoryRepository(ILogger<InMemoryRepository> logger)
        {
            _logger = logger??throw new ArgumentNullException(nameof(logger));
            _categories = new List<Category>()
            {
                new Category(){Id = 1, Name = "Notebooks"},
                new Category(){Id = 2, Name = "Tablets"}
            };
        }

        public async Task< List<Category>> GetAllCategories()
        {
            _logger.LogInformation("getting all");
            await Task.Delay(1);
            return _categories;
        }

        public Category getCategoryById(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }

        public void AddCategory(Category category)
        {
            category.Id = _categories.Max(x => x.Id) + 1;
            _categories.Add(category);
        }
    }
}
