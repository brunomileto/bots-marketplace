using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Models;

namespace BotMarketplace.Services.Interfaces
{
    public interface IBaseService<TModel>
    {
        public Task<PaginationResponse<TModel>> GetPaginationAsync(int pageNumber, int perPage);
        public Task<TModel?> GetByIdAsync(string id);
        public Task CreateAsync(TModel entity);
        public Task DeleteAsync(string id);
        public Task UpdateAsync (TModel entity); 
    }
}
