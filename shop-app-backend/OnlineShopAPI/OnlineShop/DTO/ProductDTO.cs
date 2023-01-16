namespace OnlineShop.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvalible { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Picture { get; set; }
        public string Caption { get; set; } 
        public List<CategoryDTO> Category { get; set; }
    }
}
