namespace OnlineShop.DTO
{
    public class ProductPutGetDTO
    {
        public ProductDTO Product { get; set; }
        public List <CategoryDTO> SelectedCategories { get; set; }
        public List <CategoryDTO> NonSelectedCategories { get; set; }
    }
}
