using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Category> _categories;
        public InMemoryRepository()
        {
            _categories = new List<Category>()
            {
                new Category(){Id = 1, Name = "Notebooks"},
                new Category(){Id = 2, Name = "Tablets"}
            };
        }

        public async Task< List<Category>> GetAllCategories()
        {
            await Task.Delay(1);
            return _categories;
        }

        public Category getCategoryById(int id)
        {
            return _categories.FirstOrDefault(x => x.Id == id);
        }
    }
}
