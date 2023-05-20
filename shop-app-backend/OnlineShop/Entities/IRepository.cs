namespace ShopPortal.Entities
{
    public interface IRepository
    {
        Task<List<Category>> GetAllCategories();
        public Category GetCategoryById(int id);
        public void AddCategory(Category category);
    }
}
