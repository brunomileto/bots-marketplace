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
    public class UserRepositoryTests : TestDatabaseBase<UserRepository>
    {
        private const int USERS_QUANTITY = 5;

        private Faker<User> userFaker = default!;

        protected override void SeedDatabase()
        {
            userFaker = UserFakeFactory.FakeUserMaker();

            var users = userFaker.Generate(USERS_QUANTITY);

            _context.Users.AddRange(users);

            _context.SaveChanges();
        }

        protected override UserRepository InitializeRepository()
        {
            return new UserRepository(_context);
        }

        [TestMethod]
        public async Task AddAsync_AddsUserSuccessfully()
        {
            var newUser = userFaker.Generate();
            await _repository.AddAsync(newUser);

            Assert.AreEqual(USERS_QUANTITY + 1, await _context.Users.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_RemovesUserSuccessfully()
        {
            var userToDelete = _context.Users.FirstOrDefault()!;
            await _repository.DeleteAsync(userToDelete.Id);

            Assert.AreEqual(USERS_QUANTITY - 1, await _context.Users.CountAsync());
            Assert.IsNull(await _context.Users.FindAsync(userToDelete.Id));
        }
    }
}
