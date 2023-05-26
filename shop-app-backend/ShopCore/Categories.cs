using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using ShopCore.Helpers;
using Microsoft.Extensions.Logging;
namespace ShopCore
{
   
    public class Categories
    {
        private readonly ILogger<Categories> _logger;
        private readonly ApplicationDbContext _context;
        public Categories(ILogger<Categories> logger, ApplicationDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Category>> GetAllCategoriesPaged(PaginationModel paginationModel)
        {
            _logger.LogInformation("Getting all categories paginated");
            var queryable = _context.Categories.AsQueryable();
            return  await queryable.OrderBy(x => x.Name).Paginate(paginationModel).ToListAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            _logger.LogInformation("Getting all categories");
            return await _context.Categories.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Edit(int id, Category newCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return false;
            category = newCategory;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var exitst = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!exitst)
                return false;

            _context.Remove(new Category() { Id = id });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
