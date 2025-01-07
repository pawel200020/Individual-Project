using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using ShopCore.Helpers;

namespace ShopCore;

public class Orders
{
    private readonly ApplicationDbContext _context;

    public Orders(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<(Order[] orders, int quanitity)> GetAll(PaginationModel paginationModel)
    {
        var queryable = _context.Orders.AsQueryable();
        var orders = await queryable.OrderBy(x => x.Name).Paginate(paginationModel).ToArrayAsync();
        return (orders, await queryable.CountAsync());
    }

    public async Task<Order> GetById(int id)
    {
        var order = await _context.Orders
            .Include(x => x.OrdersProducts).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            throw new InvalidOperationException("trying access order with id which not exists in database");
        return order;
    }

    private async Task RemoveOrderedProducts(int id, int quantity)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id)
                      ?? throw new InvalidOperationException("product does not exists");
        product.Quantity -= quantity;

        if (product.Quantity <= 0)
            product.IsAvalible = false;

        await _context.SaveChangesAsync();
    }

    private async Task VerifyEnoughResources(List<OrdersProducts> products)
    {
        foreach (var product in products)
        {
            var resource = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == product.ProductId);
            if (resource is null)
                throw new InvalidOperationException("Product with derived id does not exists in database");
            if (!resource.IsAvalible)
                throw new InvalidOperationException("Selected product is currently unavailable");
            if (resource.Quantity < product.Quantity)
                throw new InvalidOperationException($"there is not enough resource {resource.Name}");

            await RemoveOrderedProducts(product.ProductId, product.Quantity);
        }
    }
    private async Task<double> CountOrderValue(List<OrdersProducts> products)
    {
        double price = 0;
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

    public async Task<int> AddOrder(Order order)
    {

        await VerifyEnoughResources(order.OrdersProducts);
        order.Value = await CountOrderValue(order.OrdersProducts);
        _context.Add(order);
        await _context.SaveChangesAsync();
        return order.Id;
    }

    public async Task Delete(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            throw new InvalidOperationException("Product with derived id does not exists in database");
        _context.Remove(order);
        await _context.SaveChangesAsync();
    }

}