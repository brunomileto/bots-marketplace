using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.Core.Enums;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Tests.Factories
{
    public static class ProductFakeFactory
    {
        public static List<User> users = default!;

        public static List<Product> FakeProducts(int usersQtd = 2, int botsQtd = 5)
        {
            var botFaker = FakeProductMaker(usersQtd);

            return botFaker.Generate(botsQtd);
        }

        public static Faker<Product> FakeProductMaker(int usersQtd = 2)
        {
            var fakeUsers = UserFakeFactory.FakeUserMaker();
            users = fakeUsers.Generate(usersQtd);

            var botFaker = new Faker<Product>()
                .RuleFor(b => b.Id, f => f.Random.Uuid().ToString())
                .RuleFor(b => b.Name, f => f.Lorem.Word())
                .RuleFor(b => b.Description, f => f.Lorem.Sentence())
                .RuleFor(b => b.Price, f => f.Random.Decimal(10, 999))
                .RuleFor(b => b.CreatorId, f => f.PickRandom(users).Id)
                .RuleFor(b => b.ProductType, EnumProductType.BOT);

            return botFaker;
        }
    }
}
