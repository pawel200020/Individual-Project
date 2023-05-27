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

        public async Task<(Category[] categories, int quanitity)> GetAllCategoriesPaged(PaginationModel paginationModel)
        {
            _logger.LogInformation("Getting all categories paginated");
            var queryable = _context.Categories.AsQueryable();
            return  (await queryable.OrderBy(x => x.Name).Paginate(paginationModel).ToArrayAsync(), await queryable.CountAsync());
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

        public async Task Edit(int id, Category newCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                throw new InvalidOperationException("trying to edit not existing category");
            category = newCategory;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var exitst = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!exitst)
                throw new InvalidOperationException("trying to remove not existing category");

            _context.Remove(new Category() { Id = id });
            await _context.SaveChangesAsync();
        }
    }
}
