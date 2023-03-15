using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTO;
using OnlineShop.Entities;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string _containerName = "products";

        public ProductsController(ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [HttpGet("searchByName/{query}")]
        public async Task<ActionResult<List<ProductsOrdersDTO>>> SearchByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<ProductsOrdersDTO>();
            }

            return await _context.Products.Where(x => x.Name
                .Contains(query))
                .Where(x=>x.IsAvalible)
                .OrderBy(x => x.Name)
                .Select(x => new ProductsOrdersDTO { Id = x.Id, Name = x.Name, Picture = x.Picture })
                .Take(5)
                .ToListAsync();
        }


        [HttpGet("PostGet")]
        public async Task<ActionResult<ProductPostGetDTO>> PostGet()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);

            return new ProductPostGetDTO() { Categories = categoriesDTO };
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<ProductDTO>>> Filter([FromQuery] FilterProductsDTO filterProductsDto)
        {
            var productsQueryable = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterProductsDto.Name))
            {
                productsQueryable = productsQueryable.Where(x => x.Name.Contains(filterProductsDto.Name));
            }

            if (filterProductsDto.isAvalible)
            {
                productsQueryable = productsQueryable.Where(x => x.IsAvalible);
            }

            if (filterProductsDto.CategoryId != -1)
            {
                productsQueryable = productsQueryable
                    .Where(x => x.ProductsCategories.Select(y => y.CategoryId)
                        .Contains(filterProductsDto.CategoryId));
            }

            await HttpContext.InsertParamtersPanginationInHeader(productsQueryable);
            var products = await productsQueryable.OrderBy(x => x.Name).Paginate(filterProductsDto.PaginationDTO)
                .ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery] PaginationDTO paginationDto)
        {

            var queryable = _context.Products.AsQueryable();
            await HttpContext.InsertParamtersPanginationInHeader(queryable);
            var products = await queryable.OrderBy(x => x.Name).Paginate(paginationDto).ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _context.Products
                .Include(x => x.ProductsCategories).ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();
            return _mapper.Map<ProductDTO>(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductCreationDTO productCreationDto)
        {
            var product = _mapper.Map<Product>(productCreationDto);
            if (productCreationDto.Picture != null)
            {
                product.Picture = await _fileStorageService.SaveFile(_containerName, productCreationDto.Picture);
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("putget/{id:int}")]

        public async Task<ActionResult<ProductPutGetDTO>> PutGet(int id)
        {
            var productActionResult = await Get(id);
            if (productActionResult is NotFoundResult) return NotFound();

            var product = productActionResult.Value;
            var categoriesSelectedIds = product.Category.Select(x => x.Id).ToList();
            var nonSelectedCategories = _mapper.Map<List<CategoryDTO>>(await _context.Categories.Where(x => !categoriesSelectedIds.Contains(x.Id)).ToListAsync());

            var response = new ProductPutGetDTO();
            response.Product = product;
            response.NonSelectedCategories = nonSelectedCategories;
            response.SelectedCategories = _mapper.Map<List<CategoryDTO>>(await _context.Categories.Where(x => categoriesSelectedIds.Contains(x.Id)).ToListAsync());

            return response;

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProductCreationDTO productCreationDto)
        {
            var product = await _context.Products
                .Include(x => x.ProductsCategories).ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();
            product = _mapper.Map(productCreationDto, product);
            if (productCreationDto.Picture != null)
            {
                product.Picture = await _fileStorageService.SaveFile(_containerName, productCreationDto.Picture);
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
