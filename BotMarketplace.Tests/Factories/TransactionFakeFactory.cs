using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.API.DTOs.Transactions;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Tests.Factories
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
                .CustomInstantiator(f => new Transaction(
                    f.PickRandom(products).Id,
                    f.PickRandom(users).Id,
                    f.PickRandom(products).CreatorId,
                    f.Random.Decimal(10, 9999),
                    f.Date.Past(),
                    f.Random.Uuid().ToString(),
                    f.Date.Past()
                    ));

            return transactionFaker;

        }

        public static Faker<TransactionBaseDTO> FakeTransactionBaseDTOMaker(int usersQtd = 2, int productsQtd = 5)
        {
            var fakeUsers = UserFakeFactory.FakeUserMaker();
            var fakeProducts = ProductFakeFactory.FakeProductMaker();

            users = fakeUsers.Generate(usersQtd);
            products = fakeProducts.Generate(productsQtd);

            var faker = new Faker<TransactionBaseDTO>()
                .RuleFor(b => b.Price, f => f.Random.Decimal(10, 9999))
                .RuleFor(b => b.SellerId, f => f.PickRandom(products).CreatorId)
                .RuleFor(b => b.BuyerId, f => f.PickRandom(users).Id)
                .RuleFor(b => b.ProductId, f => f.PickRandom(products).Id);

            return faker;
        }
    }
}
