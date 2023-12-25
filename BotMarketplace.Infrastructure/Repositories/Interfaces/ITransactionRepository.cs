using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Infrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<(IEnumerable<Transaction>, int totalResults)> GetAllByUserIdAsync(string userId, int pageIndex, int pageSize);
    }
}
