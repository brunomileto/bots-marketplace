using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.Core.Models;
using BotMarketplace.Infrastructure.Data;
using BotMarketplace.Infrastructure.Repositories.Implementations;
using BotMarketplace.Tests.Factories;
using Microsoft.EntityFrameworkCore;

namespace BotMarketplace.Tests.BotMarketplace.Infrastructure.Repositories
{
    [TestClass]
    public class ProductRepositoryTests : TestDatabaseBase<ProductRepository>
    {
        private const int PRODUCTS_QUANTITY = 5;
        private const int USERS_QUANTITY = 2;

        private Faker<Product> productFaker = default!;

        protected override void SeedDatabase()
        {
            // Generate fake products
            productFaker = ProductFakeFactory.FakeProductMaker(USERS_QUANTITY);

            var products = productFaker.Generate(PRODUCTS_QUANTITY);

            var users = ProductFakeFactory.users;

            _context.Users.AddRange(users);
            _context.Products.AddRange(products);

            _context.SaveChanges();
        }

        protected override ProductRepository InitializeRepository()
        {
            return new ProductRepository(_context);
        }

        [TestMethod]
        public async Task AddAsync_AddProductsSuccessfully()
        {
            var newProduct = productFaker.Generate();

            await _repository.AddAsync(newProduct);

            Assert.AreEqual(PRODUCTS_QUANTITY + 1, await _context.Products.CountAsync());
            Assert.AreEqual(newProduct.Name, (await _context.Products.FindAsync(newProduct.Id))!.Name);
        }

        [TestMethod]
        public async Task DeleteAsync_RemoveProductSuccessfully()
        {
            var productToDelete = _context.Products.FirstOrDefault()!;

            await _repository.DeleteAsync(productToDelete.Id);

            Assert.AreEqual(PRODUCTS_QUANTITY - 1, await _context.Products.CountAsync());
            Assert.IsNull(await _context.Products.FindAsync(productToDelete.Id));
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsPaginatedResults()
        {
            var pageIndex = 1;
            var pageSize = 1;
            
            var (products, totalRecords) = await _repository.GetAllAsync(pageIndex, pageSize);
            Assert.AreEqual(pageSize, products.Count());
            Assert.AreEqual(totalRecords, PRODUCTS_QUANTITY);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectProduct()
        {
            var productToTest = await _context.Products.FirstOrDefaultAsync();

            var returnedProduct = await _repository.GetByIdAsync(productToTest!.Id);

            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(productToTest, returnedProduct);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNullForIncorrectProduct()
        {
            var returnedProduct = await _repository.GetByIdAsync(Guid.NewGuid().ToString());

            Assert.IsNull(returnedProduct);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesProductSuccessfully()
        {
            var newName = "Updated Product Name";
            var productToUpdate = await _context.Products.FirstOrDefaultAsync()!;

            productToUpdate!.Name = newName;

            await _repository.UpdateAsync(productToUpdate);

            var updatedProduct = await _context.Products.FindAsync(productToUpdate.Id);

            Assert.AreEqual(newName, updatedProduct!.Name);
        }
    }
}
