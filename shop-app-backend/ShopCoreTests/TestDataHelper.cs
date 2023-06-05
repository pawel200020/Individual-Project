using Data.Entities;

namespace ShopCoreTests
{
    internal static class TestDataHelper
    {
        public static IEnumerable<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category() {Id = 1, Name = "notebook"},
                new Category() {Id = 2, Name = "bag"},
                new Category() {Id = 3, Name = "t-shirt"},
                new Category() {Id = 4, Name = "accessories"},
                new Category() {Id = 5, Name = "tablet"},
                new Category() {Id = 6, Name = "mobile"},
                new Category() {Id = 7, Name = "wireless charger"},
                new Category() {Id = 8, Name = "display"},
                new Category() {Id = 9, Name = "vegetable"},
                new Category() {Id = 10, Name = "keyboard"},
                new Category() {Id = 11, Name = "bed"},
                new Category() {Id = 12, Name = "travel adapter"},
            };
        }

        public static IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1, Name = "Dell", IsAvalible = true, Caption = "fast laptop", Price = 4500, Quantity = 55,
                    ProductsCategories = new List<ProductsCategories>()
                },
                new Product()
                {
                    Id = 2, Name = "Asus", IsAvalible = true, Caption = "fast laptop", Price = 4500, Quantity = 4,
                    ProductsCategories = new List<ProductsCategories>()
                },
                new Product()
                {
                    Id = 3, Name = "Lenovo", IsAvalible = false, Caption = "fast laptop", Price = 4500, Quantity = 11,
                    ProductsCategories = new List<ProductsCategories>()
                },
                new Product()
                {
                    Id = 4, Name = "HP", IsAvalible = true, Caption = "fast laptop", Price = 4500, Quantity = 3,
                    ProductsCategories = new List<ProductsCategories>()
                },
                new Product()
                {
                    Id = 5, Name = "Samsung", IsAvalible = false, Caption = "fast laptop", Price = 4500, Quantity = 45,
                    ProductsCategories = new List<ProductsCategories>()
                },
                new Product()
                {
                    Id = 6, Name = "Ikea", IsAvalible = true, Caption = "fast bag", Price = 4500, Quantity = 23,
                    ProductsCategories = new List<ProductsCategories>()
                }

            };
        }

        public static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1, 
                    Name = "first",
                    OrdersProducts = new List<OrdersProducts>()
                    {
                        new OrdersProducts() {OrderId = 1, ProductId = 1, Quantity = 2},
                        new OrdersProducts() {OrderId = 1, ProductId = 4, Quantity = 4},
                    }
                },
                new Order
                {
                    Id = 2,
                    Name = "Second",
                    OrdersProducts = new List<OrdersProducts>()
                    {
                         new OrdersProducts() {OrderId = 2, ProductId = 6, Quantity = 2}, 
                         new OrdersProducts() {OrderId = 2, ProductId = 5, Quantity = 9},
                    }
                },
                new Order
                {
                    Id = 3,
                    Name = "third",
                    OrdersProducts = new List<OrdersProducts>()
                    {
                        new OrdersProducts() {OrderId = 3, ProductId = 5, Quantity = 5},
                        new OrdersProducts() {OrderId = 3, ProductId = 7, Quantity = 11},
                    }
                }
            };

        }
    }
}

