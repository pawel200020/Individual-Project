using ViewModels.Shop.Products;

namespace ViewModels.Shop.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public List<ProductsOrdersViewModel>? OrdersProducts { get; set; }
    }
}
