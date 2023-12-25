using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Models;
using BotMarketplace.Infrastructure.Repositories.Interfaces;
using BotMarketplace.Services.Interfaces;

namespace BotMarketplace.Services.Implementations
{
    public class ProductService : BaseService<Product, IProductRepository>, IProductService
    {
        public ProductService(IProductRepository repository) : base(repository)
        {
        }
    }
}
