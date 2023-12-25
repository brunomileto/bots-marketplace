using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Common.Extensions;
using BotMarketplace.Core.Models;
using BotMarketplace.Infrastructure.Data;
using BotMarketplace.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BotMarketplace.Infrastructure.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketplaceContext _context;

        public ProductRepository(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Product>, int totalResults)> GetAllAsync(int pageIndex, int pageSize)
        {
            var (products, totalResults) = await _context.Products
                                    .OrderBy(p => p.DateCreated)
                                    .PaginateAsync(pageIndex, pageSize);

            return (products, totalResults);
        }

        public async Task<Product?> GetByIdAsync(string productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();  
        }
    }
}
