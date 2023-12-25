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
    // TransactionService extends BaseService and implements ITransactionService
    // BaseSservice expects a model <Transaction> and a repository that comes from an IBaseRepository <ITransactionRepository>
    public class TransactionService : BaseService<Transaction, ITransactionRepository>, ITransactionService
    {
        public TransactionService(ITransactionRepository repository) : base(repository)
        {
        }

        public async Task<PaginationResponse<Transaction>> GetAllTransactionsByUserIdAsync(string userId, int pageIndex, int pageSize)
        {
            var (transactions, totalResults) = await _repository.GetAllByUserIdAsync(userId, pageIndex, pageSize);

            var response = new PaginationResponse<Transaction>(transactions, totalResults, pageIndex, pageSize);

            return response;
        }
    }
}
