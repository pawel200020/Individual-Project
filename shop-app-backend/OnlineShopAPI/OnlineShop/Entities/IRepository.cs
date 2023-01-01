namespace OnlineShop.Entities
{
    public interface IRepository
    {
        Task<List<Category>> GetAllCategories();
        public Category getCategoryById(int id);
        public void AddCategory(Category category);
    }
}
