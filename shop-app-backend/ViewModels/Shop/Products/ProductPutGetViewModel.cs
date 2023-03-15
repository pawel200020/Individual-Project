using ViewModels.Shop.Categories;

namespace ViewModels.Shop.Products
{
    public class ProductPutGetViewModel
    {
        public ProductViewModel Product { get; set; } = null!;
        public List<CategoryViewModel> SelectedCategories { get; set; } = null!;
        public List<CategoryViewModel> NonSelectedCategories { get; set; } = null!;
    }
}
