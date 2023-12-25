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
    public class UserRepository : IUserRepository
    {
        private readonly MarketplaceContext _context;


        public UserRepository(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<User>, int totalResults)> GetAllAsync(int pageIndex, int pageSize)
        {
            var (users, totalRecords) = await _context.Users
                                        .OrderBy(p => p.DateCreated)
                                        .PaginateAsync(pageIndex, pageSize);

            return (users, totalRecords);
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
