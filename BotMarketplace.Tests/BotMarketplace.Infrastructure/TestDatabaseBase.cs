using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BotMarketplace.Tests.BotMarketplace.Infrastructure
{
    public abstract class TestDatabaseBase<T>
    {
        protected MarketplaceContext _context = default!;
        protected T _repository = default!;

        [TestInitialize]
        public void Initialize()
        {
            _context = CreateContext();
            SeedDatabase();
            _repository = InitializeRepository();
        }

        private MarketplaceContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new MarketplaceContext(options);
            return context;
        }

        protected abstract void SeedDatabase();

        protected abstract T InitializeRepository();

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
