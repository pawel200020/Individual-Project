using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop;
using ShopPortal.Entities;
using ShopPortal.Helpers;
using ViewModels.Pagination;
using ViewModels.Shop.Categories;
using ViewModels.Shop.Products;

#pragma warning disable CS0184

namespace ShopPortal.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string _containerName = "products";

        public ProductsController(ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService, UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _userManager = userManager;
        }

        [HttpGet("searchByName/{query}")]
        public async Task<ActionResult<List<ProductsOrdersViewModel>>> SearchByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<ProductsOrdersViewModel>();
            }

            return await _context.Products.Where(x => x.Name
                .Contains(query))
                .Where(x => x.IsAvalible)
                .OrderBy(x => x.Name)
                .Select(x => new ProductsOrdersViewModel { Id = x.Id, Name = x.Name, Picture = x.Picture })
                .Take(5)
                .ToListAsync();
        }


        [HttpGet("PostGet")]
        public async Task<ActionResult<ProductPostGetViewModel>> PostGet()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoriesDTO = _mapper.Map<List<CategoryViewModel>>(categories);

            return new ProductPostGetViewModel() { Categories = categoriesDTO };
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductViewModel>>> Filter([FromQuery] FilterProductsViewModel filterProductsViewModel)
        {
            var productsQueryable = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterProductsViewModel.Name))
            {
                productsQueryable = productsQueryable.Where(x => x.Name.Contains(filterProductsViewModel.Name));
            }

            if (filterProductsViewModel.isAvalible)
            {
                productsQueryable = productsQueryable.Where(x => x.IsAvalible);
            }

            if (filterProductsViewModel.CategoryId != -1)
            {
                productsQueryable = productsQueryable
                    .Where(x => x.ProductsCategories.Select(y => y.CategoryId)
                        .Contains(filterProductsViewModel.CategoryId));
            }

            await HttpContext.InsertParametersPaginationInHeader(productsQueryable);
            var products = await productsQueryable.OrderBy(x => x.Name).Paginate(filterProductsViewModel.PaginationViewModel)
                .ToListAsync();
            return _mapper.Map<List<ProductViewModel>>(products);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductViewModel>>> Get([FromQuery] PaginationViewModel paginationViewModel)
        {

            var queryable = _context.Products.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var products = await queryable.OrderBy(x => x.Name).Paginate(paginationViewModel).ToListAsync();
            return _mapper.Map<List<ProductViewModel>>(products);
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            var product = await _context.Products
                .Include(x => x.ProductsCategories).ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();

            var avgVote = 0.0;
            var userVote = 0;

            if(await _context.Rating.AnyAsync(x=>x.ProductId == id))
            {
                avgVote = await _context.Rating.Where(x => x.ProductId == id).AverageAsync(x => x.Rate);
                if (HttpContext.User.Identity is {IsAuthenticated: true})
                {
                    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                    var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
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

            var productModel = _mapper.Map<ProductViewModel>(product);
            productModel.AverageVote = avgVote;
            productModel.UserVote = userVote;
            return productModel;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductCreationViewModel productCreationViewModel)
        {
            var product = _mapper.Map<Product>(productCreationViewModel);
            if (productCreationViewModel.Picture != null)
            {
                product.Picture = await _fileStorageService.SaveFile(_containerName, productCreationViewModel.Picture);
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("putget/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductPutGetViewModel>> PutGet(int id)
        {
            var productActionResult = await Get(id);
            if (productActionResult is NotFoundResult) 
                return NotFound();

            var product = productActionResult.Value;
            var categoriesSelectedIds = product?.Category?.Select(x => x.Id).ToArray() ?? Array.Empty<int>();
            var nonSelectedCategories = _mapper.Map<List<CategoryViewModel>>(await _context.Categories.Where(x => !categoriesSelectedIds.Contains(x.Id)).ToListAsync());

            return new ProductPutGetViewModel
            {
                Product = product ?? throw new ArgumentNullException(nameof(product),"Product not found in database"),
                NonSelectedCategories = nonSelectedCategories,
                SelectedCategories = _mapper.Map<List<CategoryViewModel>>(await _context.Categories.Where(x => categoriesSelectedIds.Contains(x.Id)).ToListAsync())
            };
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProductCreationViewModel productCreationViewModel)
        {
            var product = await _context.Products
                .Include(x => x.ProductsCategories).ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();
            product = _mapper.Map(productCreationViewModel, product);
            if (productCreationViewModel.Picture != null)
            {
                product.Picture = await _fileStorageService.SaveFile(_containerName, productCreationViewModel.Picture);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }

            return NoContent();


        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
