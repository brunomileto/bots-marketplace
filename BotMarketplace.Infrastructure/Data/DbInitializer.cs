using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMarketplace.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MarketplaceContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
