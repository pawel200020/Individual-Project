using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCore;
using ShopPortal.Helpers;
using ViewModels.Pagination;
using ViewModels.Shop.Categories;
using ViewModels.Shop.Products;


namespace ShopPortal.Controllers
{
    /// <summary>
    /// Controller responsible for products in a shop
    /// </summary>
    [Route("api/products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Products _products;
        private readonly ILogger<ProductsController> _logger;


        /// <inheritdoc />
        public ProductsController(IMapper mapper, Products products, ILogger<ProductsController> logger)
        {
            _mapper = mapper ;
            _products = products?? throw new ArgumentNullException(nameof(products));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// search product by name
        /// </summary>
        /// <param name="query"></param>
        /// <returns>found product</returns>
        [HttpGet("searchByName/{query}")]
        public async Task<ActionResult<ProductsOrdersViewModel[]>> SearchByName(string query)
        {
            var result = await _products.SearchByName(query);
            return _mapper.Map<ProductsOrdersViewModel[]>(result);
        }

        /// <summary>
        /// Get list of categories to create a new product
        /// </summary>
        /// <returns> new product with possible categories</returns>
        [HttpGet("PostGet")]
        public async Task<ActionResult<ProductPostGetViewModel>> PostGet()
        {
            var categories = _mapper.Map<CategoryViewModel[]>(await _products.GetEmptyProductWithAllCategories());
            return new ProductPostGetViewModel() { Categories = categories };
        }

        /// <summary>
        /// Filter products by search criteria
        /// </summary>
        /// <param name="filterProductsViewModel"></param>
        /// <returns>list of filtered products</returns>
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductViewModel[]>> Filter([FromQuery] FilterProductsViewModel filterProductsViewModel)
        {
            var (result, quantity) = await _products.FilterWithCriteria(_mapper.Map<FilterProducts>(filterProductsViewModel));
            HttpContext.InsertParametersPaginationInHeader(quantity);
            return _mapper.Map<ProductViewModel[]>(result);
        }

        /// <summary>
        /// Get list of products with pagination
        /// </summary>
        /// <param name="paginationViewModel"></param>
        /// <returns>list of products without list of categories!</returns>
        [HttpGet]
        public async Task<ActionResult<ProductViewModel[]>> Get([FromQuery] PaginationViewModel paginationViewModel)
        {
            var (result, quantity) = await _products.Get(_mapper.Map<PaginationModel>(paginationViewModel));
            HttpContext.InsertParametersPaginationInHeader(quantity);
            return _mapper.Map<ProductViewModel[]>(result);
        }

        /// <summary>
        /// Get a product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>found product</returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            var result = await _products.GetById(id, GetCurrentlyLoggedUserMail());
            if (result == null)
            {
                _logger.LogWarning("product does not exists in database");
                return NotFound();
            }
                

            return _mapper.Map<ProductViewModel>(result);
        }

        /// <summary>
        /// new product
        /// </summary>
        /// <param name="productCreationViewModel"></param>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductCreationViewModel productCreationViewModel)
        {
            await _products.Create(_mapper.Map<Product>(productCreationViewModel));
            return NoContent();
        }

        /// <summary>
        /// Get product to edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>product to edit</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("putget/{id:int}")]
        public async Task<ActionResult<ProductPutGetViewModel>> PutGet(int id)
        {
            try
            {
                var result = await _products.PrepareForEdit(id, GetCurrentlyLoggedUserMail());
                return _mapper.Map<ProductPutGetViewModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
            return NoContent();
        }

        /// <summary>
        /// Insert edited product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productCreationViewModel"></param>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProductCreationViewModel productCreationViewModel)
        {
            try
            {
                await _products.Save(id, _mapper.Map<Product>(productCreationViewModel));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
            return NoContent();
        }
        /// <summary>
        /// Delete product with selected ID
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _products.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }

            return NoContent();
        }

        #region UserAccountsHelperMethods
        private string? GetCurrentlyLoggedUserMail()
        {
            string? email = null;
            if (HttpContext.User.Identity is { IsAuthenticated: true })
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                email = HttpContext.User.Claims.FirstOrDefault(x =>
                    x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            }
            return email;
        }
        #endregion
    }
}
