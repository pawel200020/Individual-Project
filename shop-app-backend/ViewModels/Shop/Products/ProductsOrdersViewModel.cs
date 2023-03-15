namespace ViewModels.Shop.Products
{
    public class ProductsOrdersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Picture { get; set; }
        public int Quantity { get; set; }
    }
}
