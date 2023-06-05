using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopCore.Helpers;

namespace ShopCore
{
    public class Products
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IFileStorageService _fileStorageService;
        private readonly string _containerName = "products";

        public Products(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            IFileStorageService fileStorageService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
        }

        public async Task<ProductsOrders[]> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Array.Empty<ProductsOrders>();
            }

            return await _context.Products.Where(x => x.Name
                    .Contains(name))
                .Where(x => x.IsAvalible)
                .OrderBy(x => x.Name)
                .Select(x => new ProductsOrders {Id = x.Id, Name = x.Name, Picture = x.Picture})
                .Take(5)
                .ToArrayAsync();
        }

        public async Task<Category[]> GetEmptyProductWithAllCategories()
        {
            return await _context.Categories.ToArrayAsync();
        }

        public async Task<(Product[]products, int quantity)> FilterWithCriteria(FilterProducts filterProducts)
        {
            var productsQueryable = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterProducts.Name))
            {
                productsQueryable = productsQueryable.Where(x => x.Name.Contains(filterProducts.Name));
            }

            if (filterProducts.isAvalible)
            {
                productsQueryable = productsQueryable.Where(x => x.IsAvalible);
            }

            if (filterProducts.CategoryId != -1)
            {
                productsQueryable = productsQueryable
                    .Where(x => x.ProductsCategories.Select(y => y.CategoryId)
                        .Contains(filterProducts.CategoryId));
            }

            var products = await productsQueryable.OrderBy(x => x.Name).Paginate(filterProducts.PaginationModel)
                .ToArrayAsync();
            return (products, await productsQueryable.CountAsync());
        }

        public async Task<(Product[] products, int quantity)> Get(PaginationModel paginationModel)
        {
            var queryable = _context.Products.AsQueryable();
            var products = await queryable.OrderBy(x => x.Name).Paginate(paginationModel).ToArrayAsync();
            return (products, await queryable.CountAsync());
        }

        public async Task<Product?> GetById(int id, string? email)
        {
            var product = await _context.Products
                .Include(x => x.ProductsCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return null;

            var avgVote = 0.0;
            var userVote = 0;

            if (await _context.Rating.AnyAsync(x => x.ProductId == id))
            {
                avgVote = await _context.Rating.Where(x => x.ProductId == id).AverageAsync(x => x.Rate);
                if (email is not null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    var userId = user.Id;

                    var ratingDb =
                        await _context.Rating.FirstOrDefaultAsync(x => x.ProductId == id && x.UserId == userId);
                    if (ratingDb != null)
                    {
                        userVote = ratingDb.Rate;
                    }
                }
            }

            product.AverageVote = avgVote;
            product.UserVote = userVote;
            return product;
        }

        public async Task Create(Product product)
        {
            if (product.PictureFile != null)
            {
                product.Picture = await _fileStorageService.SaveFile(_containerName, product.PictureFile);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductPutGet> PrepareForEdit(int id, string? email)
        {
            var product = await GetById(id, email);

            var categoriesSelectedIds =
                product?.ProductsCategories?.Select(x => x.ProductId).ToArray() ?? Array.Empty<int>();
            var selectedCategories =
                await _context.Categories.Where(x => categoriesSelectedIds.Contains(x.Id)).ToArrayAsync();
            var nonSelectedCategories =
                await _context.Categories.Where(x => !categoriesSelectedIds.Contains(x.Id)).ToArrayAsync();

            return new ProductPutGet()
            {
                Product = product ?? throw new ArgumentNullException(nameof(product), "Product not found in database"),
                SelectedCategories = selectedCategories,
                NonSelectedCategories = nonSelectedCategories
            };
        }

        public async Task Save(int id, Product editedProduct)
        {
            var product = await _context.Products
                .Include(x => x.ProductsCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
                throw new InvalidOperationException("trying to edit not existing product");

            if (editedProduct.PictureFile != null)
                product.Picture = await _fileStorageService.SaveFile(_containerName, editedProduct.PictureFile);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                throw new InvalidOperationException("product which you attempted to remove does not exits");

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
