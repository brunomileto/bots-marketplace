using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
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
    }
}
