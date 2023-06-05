using Data.Entities;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopCore;
using Moq.EntityFrameworkCore;
using ShopCore.Helpers;

namespace ShopCoreTests
{
    [TestFixture]
    internal class ProductsTests
    {
        private Products _products;
        private Mock<ApplicationDbContext> _contextMock;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        [SetUp]
        public void Setup()
        {
            var fileserviceMock = new Mock<IFileStorageService>();
            _contextMock = new Mock<ApplicationDbContext>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _products = new Products(_contextMock.Object, _userManagerMock.Object,fileserviceMock.Object);
            _contextMock.Setup<DbSet<Product>>(x => x.Products)
                .ReturnsDbSet(TestDataHelper.GetProducts());
        }

        [TestCase(15)]
        [TestCase(35)]
        [TestCase(51)]
        public void TestNonExistingSave(int id)
        {
            var product = new Mock<Product>();
            Assert.ThrowsAsync<InvalidOperationException>(() => _products.Save(id,product.Object));
        }

        [TestCase(15)]
        [TestCase(35)]
        [TestCase(51)]
        public void TestNonExistingDelete(int id)
        {
            var product = new Mock<Product>();
            Assert.ThrowsAsync<InvalidOperationException>(() => _products.Save(id, product.Object));
        }

    }
}
