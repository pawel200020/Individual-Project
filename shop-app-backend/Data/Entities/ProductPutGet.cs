namespace Data.Entities
{
    public class ProductPutGet
    {
        public Product Product { get; set; } = null!;
        public Category[] SelectedCategories { get; set; } = null!;
        public Category[] NonSelectedCategories { get; set; } = null!;
    }
}
