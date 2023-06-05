using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using ShopCore;

namespace ShopCoreTests
{
    [TestFixture]
    internal class CategoriesTests
    {
        private Categories _categories;
        private Mock<ApplicationDbContext> _contextMock;
        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<Categories>>();
            _contextMock = new Mock<ApplicationDbContext>();
            _categories = new Categories(loggerMock.Object, _contextMock.Object);
            _contextMock.Setup<DbSet<Category>>(x => x.Categories)
                .ReturnsDbSet(TestDataHelper.GetCategories());
        }
        //TODO Integration tests
        //[TestCase(1,5,5)]
        //[TestCase(2,5,5)]
        //[TestCase(3,5,3)]
        //public void CheckPaginationQuantityResult(int page, int recordsPerPage,int result)
        //{
        //    var paginationModel = new Mock<PaginationModel>();
        //    paginationModel.Setup(m => m.Page).Returns(page);
        //    paginationModel.Setup(m => m.RecordsPerPage).Returns(recordsPerPage);

        //    var (list,quantity) = _categories.GetAllCategoriesPaged(paginationModel.Object).Result;
        //    Assert.That(quantity, Is.EqualTo(result));

        //}

        [TestCase(12)]
        public void GetAllCategories(int expected)
        {
            var result = _categories.GetAllCategories().Result;
            Assert.That(expected, Is.EqualTo(result.Count));
        }
        [TestCase(1, "notebook")]
        [TestCase(4, "accessories")]
        [TestCase(5, "tablet")]
        [TestCase(7, "wireless charger")]
        [TestCase(9, "vegetable")]
        public void GetByIdExists(int id, string expected)
        {
            var result = _categories.GetById(id).Result;
            if (result != null) 
                Assert.That(expected, Is.EqualTo(result.Name));
        }
        [TestCase(22)]
        [TestCase(54)]
        [TestCase(58)]
        [TestCase(47)]
        [TestCase(98)]
        public void EditNotExistingCategory(int id)
        {
            Assert.ThrowsAsync<InvalidOperationException>(()=>_categories.Edit(id, new Category()));
        }
        [TestCase(22)]
        [TestCase(54)]
        [TestCase(58)]
        [TestCase(47)]
        [TestCase(98)]
        public void DeleteNotExistingCategory(int id)
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => _categories.Delete(id));
        }
    }
}
