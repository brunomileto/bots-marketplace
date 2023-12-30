using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BotMarketplace.Tests.BotMarketplace.API
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MarketplaceContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<MarketplaceContext>(options =>
                {
                    options.UseInMemoryDatabase("BotMarketplaceDB");
                });
            });
        }

        public virtual string GetPaginatedParams(int pageNumber, int perPage)
        {
            var parameters = new Dictionary<string, string>
            {
                {"pageNumber", pageNumber.ToString() },
                {"perPage", perPage.ToString() }
            };

            var builder = new StringBuilder();
            foreach (var param in parameters)
            {
                if (builder.Length > 0)
                    builder.Append("&");

                var key = WebUtility.UrlEncode(param.Key);
                var value = WebUtility.UrlEncode(param.Value);
                builder.Append($"{key}={value}");
            }

            return builder.ToString();
        }

    }

    public delegate string GetPaginatedParamsDelegate(int pageNumber, int perPage);
}
