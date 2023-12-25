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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MarketplaceContext _context;

        public TransactionRepository(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Transaction>, int totalResults)> GetAllAsync(int pageIndex, int pageSize)
        {
            var (transacations, totalRecords) = await _context.Transactions
                                                .OrderBy(p => p.DateCreated)
                                                .PaginateAsync(pageIndex, pageSize);

            return (transacations, totalRecords);
        }

        public async Task<(IEnumerable<Transaction>, int totalResults)> GetAllByUserIdAsync(string userId, int pageIndex, int pageSize)
        {
            var (transactions, totalRecords) = await _context.Transactions
                                                .OrderBy(p => p.DateCreated)
                                                .PaginateAsync(pageIndex, pageSize);

            return (transactions, totalRecords);
        }

        public async Task<Transaction?> GetByIdAsync(string transactionId)
        {
            return await _context.Transactions.FindAsync(transactionId);
        }

        public async Task UpdateAsync(Transaction item)
        {
            return;
        }

        public async Task DeleteAsync(string id)
        {
            return;
        }
    }
}
