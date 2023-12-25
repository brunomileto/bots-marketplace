using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Tests.BotMarketplace.Infrastructure.Factories
{
    public static class TransactionFakeFactory
    {
        public static List<User> users = default!;

        public static List<Product> products = default!;

        public static List<Transaction> FakeTransactions(int usersQtd = 2, int productsQtd = 5, int transactionsQtd = 10)
        {
            var transactionFaker = FakeTransactionMaker(usersQtd, productsQtd);

            return transactionFaker.Generate(transactionsQtd);
        }

        public static Faker<Transaction> FakeTransactionMaker(int usersQtd = 2, int productsQtd = 5)
        {
            users = UserFakeFactory.FakeUserMaker().Generate(usersQtd);
            products = ProductFakeFactory.FakeProductMaker().Generate(productsQtd);

            var transactionFaker = new Faker<Transaction>()
                .RuleFor(t => t.Id, f => f.Random.Uuid().ToString())
                .RuleFor(t => t.TransactionDate, f => f.Date.Past())
                .RuleFor(t => t.Price, f => f.Random.Decimal(10, 9999))
                .RuleFor(t => t.ProductId, f => f.PickRandom(products).Id)
                .RuleFor(t => t.SellerId, f => f.PickRandom(products).CreatorId)
                .RuleFor(t => t.BuyerId, f => f.PickRandom(users).Id);

            return transactionFaker;

        }
    }
}
