using Eshop.Core;
using Eshop.Core.DAL;
using Eshop.Core.Mocks;
using Eshop.Core.Models.Database;
using Eshop.Core.Services;
using Eshop.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.Services.Tests
{
    public class Tests
    {
        private IProductService _productService;
        private IProductDAL _productDAL;
        private EshopDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EshopDbContext>().UseInMemoryDatabase("EshopDB").Options;
            _dbContext = new EshopDbContext(options);

            _productDAL = new ProductDAL(_dbContext);
            _productService = new ProductService(_productDAL);
        }

        [Test]
        public async Task EmptyDB_GetAll_EmptyCollectionReturned()
        {
            IEnumerable<Product> ret = await _productService.GetAll().ConfigureAwait(false);

            Assert.IsEmpty(ret);
        }

        [Test]
        public async Task EmptyDB_GetNotExistingProduct_NullReturned()
        {
            Product ret = await _productService.Get(10).ConfigureAwait(false);

            Assert.IsNull(ret);
        }

        [Test]
        public async Task MockDB_GetExistingProduct_ValidProductReturned()
        {
            _dbContext.InitializeTestDatabase();
            Product ret = await _productService.Get(1).ConfigureAwait(false);

            Assert.NotNull(ret);
        }

        [Test]
        public async Task MockDB_GetAll_ProductsReturned()
        {
            _dbContext.InitializeTestDatabase();
            IEnumerable<Product> ret = await _productService.GetAll().ConfigureAwait(false);

            Assert.IsNotEmpty(ret);
        }

        [Test]
        public async Task CreateProduct_ChangeDescription_ChangedSuccessfully()
        {
            Product product = new Product()
            {
                Id = 7,
                Description = "Old desciption",
                Name = "Product",
                ImgUri = "",
                Price = 10
            };
            string newDescription = "New description";

            _dbContext.Add(product);

            await _productService.EditDescription(product.Id, newDescription).ConfigureAwait(false);

            Product ret = await _productService.Get(product.Id).ConfigureAwait(false);

            Assert.AreEqual(newDescription, ret.Description);
        }

    }
}