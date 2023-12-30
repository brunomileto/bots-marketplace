using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.Core.Models;
using BotMarketplace.Infrastructure.Repositories.Implementations;
using BotMarketplace.Tests.Factories;
using Microsoft.EntityFrameworkCore;

namespace BotMarketplace.Tests.BotMarketplace.Infrastructure.Repositories
{
    [TestClass]
    public class TransactionRepositoryTests : TestDatabaseBase<TransactionRepository>
    {
        private const int BOTS_QUANTITY = 5;
        private const int USERS_QUANTITY = 4;
        private const int TRANSACTIONS_QUANTITY = 12;

        private Faker<Transaction> transactionFaker = default!;

        protected override TransactionRepository InitializeRepository()
        {
            return new TransactionRepository(_context);
        }

        protected override void SeedDatabase()
        {
            transactionFaker = TransactionFakeFactory.FakeTransactionMaker(USERS_QUANTITY, BOTS_QUANTITY);

            var transactions = transactionFaker.Generate(TRANSACTIONS_QUANTITY);

            var users = TransactionFakeFactory.users;
            var products = TransactionFakeFactory.products;

            _context.Users.AddRange(users);
            _context.Products.AddRange(products);
            _context.Transactions.AddRange(transactions);

            _context.SaveChanges();
        }

        [TestMethod]
        public async Task AddAsync_AddsTransactionSuccessfully()
        {
            var newTransaction = transactionFaker.Generate();

            await _repository.AddAsync(newTransaction);

            Assert.AreEqual(TRANSACTIONS_QUANTITY + 1, await _context.Transactions.CountAsync());
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsPaginatedResults()
        {
            var pageIndex = 1;
            var pageSize = 1;

            var (transactions, totalResults) = await _repository.GetAllAsync(pageIndex, pageSize);

            Assert.AreEqual(pageSize, transactions.Count());
            Assert.AreEqual(TRANSACTIONS_QUANTITY, totalResults);
        }

        [TestMethod]
        public async Task GetAllByUserIdAsync_ReturnsPaginatedResultsByUser()
        {
            var pageIndex = 1;
            var pageSize = 20;
            var transactionToTestTheUser = _context.Transactions.FirstOrDefault()!;

            var (transactions, totalResults) = await _repository.GetAllByUserIdAsync(transactionToTestTheUser.SellerId, pageIndex, pageSize);

            Assert.IsNotNull(transactions.Where(t => t.Id == transactionToTestTheUser.Id).ToList()[0]);
            Assert.AreEqual(TRANSACTIONS_QUANTITY, totalResults);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectTransaction()
        {
            var transactionToTest = _context.Transactions.FirstOrDefault()!;
            var transaction = await _repository.GetByIdAsync(transactionToTest.Id);

            Assert.AreEqual(transactionToTest.Id, transaction!.Id);
        }
    }

}
