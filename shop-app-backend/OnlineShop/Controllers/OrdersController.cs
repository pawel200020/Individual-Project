using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopCore;
using ShopPortal.Helpers;
using ViewModels.Pagination;
using ViewModels.Shop.Orders;

namespace ShopPortal.Controllers
{
    /// <summary>
    /// Controller responsible for order management in shop
    /// </summary>
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Orders _orders;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        /// <inheritdoc />
        public OrdersController(IMapper mapper, Orders orders, ILogger<OrdersController> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orders = orders ?? throw new ArgumentNullException(nameof(orders));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a page with orders
        /// </summary>
        /// <param name="paginationViewModel"></param>
        /// <returns>list with derived number of orders</returns>
        [HttpGet]
        public async Task<ActionResult<OrderViewModel[]>> Get([FromQuery] PaginationViewModel paginationViewModel)
        {
            var( orders, quantity) = await _orders.GetAll(_mapper.Map<PaginationModel>(paginationViewModel));
            HttpContext.InsertParametersPaginationInHeader(quantity);
            return _mapper.Map<OrderViewModel[]>(orders);
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>order with selected ID</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderViewModel>> Get(int id)
        {
            try
            {
                var order = await _orders.GetById(id);
                return _mapper.Map<OrderViewModel>(order);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Add new order into database
        /// </summary>
        /// <param name="orderCreationViewModel"></param>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] OrderCreationViewModel orderCreationViewModel)
        {

            try
            {
                var createdOrderId = await _orders.AddOrder(_mapper.Map<Order>(orderCreationViewModel));
                return createdOrderId;
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new object []{ex.Message});
            }
        }

        /// <summary>
        /// Delete order with derived ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orders.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new object[] { ex.Message });
            }
        }
    }
}
