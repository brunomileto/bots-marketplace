using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Tests.BotMarketplace.Infrastructure.Factories
{
    public static class UserFakeFactory
    {

        public static List<User> FakeUsers(int userQtd = 2)
        {
            var userFaker = FakeUserMaker();

            return userFaker.Generate(userQtd);
        }

        public static Faker<User> FakeUserMaker()
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Uuid().ToString())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email());

            return userFaker;
        }


    }
}
