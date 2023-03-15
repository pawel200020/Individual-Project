﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTO;
using OnlineShop.Entities;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> Get([FromQuery] PaginationDTO paginationDto)
        {

            var queryable = _context.Orders.AsQueryable();
            await HttpContext.InsertParamtersPanginationInHeader(queryable);
            var orders = await queryable.OrderBy(x => x.Name).Paginate(paginationDto).ToListAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            var order = await _context.Orders
                .Include(x=>x.OrdersProducts).ThenInclude(x=>x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null) return NotFound();
            return _mapper.Map<OrderDTO>(order);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] OrderCreationDTO orderCreationDto)
        {
            var order = _mapper.Map<Order>(orderCreationDto);
            order.Value = await CountOrderValue(order.OrdersProducts);
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductCreationDTO productCreationDto)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null) return NotFound();
            _context.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<double> CountOrderValue(List<OrdersProducts> products)
        {
            double price = 0;
            if(products == null) return price;
            foreach (var product in products)
            {
                var founded = await _context.Products
                    .Include(x => x.ProductsCategories).ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == product.ProductId);
                if (founded != null)
                {
                    price += product.Quantity * founded.Price;
                }
            }
            return price;
        }

    }
}