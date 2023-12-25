using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Services.Interfaces
{
    public interface ITransactionService :  IBaseService<Transaction>
    {
        Task<PaginationResponse<Transaction>> GetAllTransactionsByUserIdAsync(string userId, int pageIndex, int pageSize);
    }
}
