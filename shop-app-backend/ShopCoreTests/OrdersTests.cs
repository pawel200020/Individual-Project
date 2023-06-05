using Data.Entities;
using Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopCore;
using Moq.EntityFrameworkCore;

namespace ShopCoreTests
{
    internal class OrdersTests
    {
        [TestFixture]
        internal class ProductsTests
        {
            private Orders _orders;
            private Mock<ApplicationDbContext> _contextMock;

            [SetUp]
            public void Setup()
            {
                _contextMock = new Mock<ApplicationDbContext>();
                _orders = new Orders(_contextMock.Object);
                _contextMock.Setup<DbSet<Order>>(x => x.Orders)
                    .ReturnsDbSet(TestDataHelper.GetOrders());
                _contextMock.Setup<DbSet<Product>>(x => x.Products)
                    .ReturnsDbSet(TestDataHelper.GetProducts());
            }

            [TestCase(4,2)]
            [TestCase(1,2)]
            [TestCase(6,4)]
            public void CreateOrderWithEnoughResourcesTests(int productId, int quanitity)
            {
                var orderedProducts = new List<OrdersProducts>()
                {
                    new OrdersProducts() {ProductId = productId, Quantity = quanitity}
                };
                var result = _orders.AddOrder(new Order {Name = "Test", OrdersProducts = orderedProducts}).Result;
                Assert.Pass();
            }
            [TestCase(4, 7)]
            [TestCase(1, 102)]
            [TestCase(6, 504)]
            public void CreateOrderWithNoEnoughResourcesTests(int productId, int quanitity)
            {
                var orderedProducts = new List<OrdersProducts>()
                {
                    new OrdersProducts() {ProductId = productId, Quantity = quanitity}
                };
                var ex = Assert.ThrowsAsync<InvalidOperationException>(()=> _orders.AddOrder(new Order { Name = "Test", OrdersProducts = orderedProducts }));
                Assert.True(ex.Message.Contains("there is not enough resource"));
            }
            [TestCase(3, 3, ExpectedResult = "Selected product is currently unavailable")]
            [TestCase(5, 2, ExpectedResult = "Selected product is currently unavailable")]
            [TestCase(56, 2,ExpectedResult = "Product with derived id does not exists in database")]
            [TestCase(94, 2, ExpectedResult = "Product with derived id does not exists in database")]
            public string CreateOrderWithNoAvalibleOrNotExistsResourcesTests(int productId, int quanitity)
            {
                var orderedProducts = new List<OrdersProducts>()
                {
                    new OrdersProducts() {ProductId = productId, Quantity = quanitity}
                };
                var ex = Assert.ThrowsAsync<InvalidOperationException>(() => _orders.AddOrder(new Order { Name = "Test", OrdersProducts = orderedProducts }));
                return ex == null
                    ? ""
                    : ex.Message;
            }
            [TestCase(42)]
            [TestCase(77)]
            [TestCase(44)]
            public void RemoveNotExistingOrder(int id)
            {
                Assert.ThrowsAsync<InvalidOperationException>(()=>_orders.Delete(id));
            }

        }

    }
}
