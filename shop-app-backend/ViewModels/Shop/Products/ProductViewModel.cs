using ViewModels.Shop.Categories;

namespace ViewModels.Shop.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsAvalible { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string? Picture { get; set; }
        public string? Caption { get; set; } 
        public List<CategoryViewModel>? Category { get; set; }
    }
}
