using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.API.DTOs.Users;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Tests.Factories
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
                .CustomInstantiator(f => new User(
                        f.Random.Uuid().ToString(),
                        f.Date.Past(),
                        f.Name.FullName(),
                        f.Internet.Email()
                    ));

            return userFaker;
        }

        public static Faker<UserBaseDTO> FakeUserBaseDTOMaker()
        {
            var faker = new Faker<UserBaseDTO>()
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email());

            return faker;
        }
    }
}
