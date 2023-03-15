using Microsoft.AspNetCore.Mvc;
using OnlineShop.Helpers;

namespace OnlineShop.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public List<ProductsOrdersDTO> OrdersProducts { get; set; }
    }
}
