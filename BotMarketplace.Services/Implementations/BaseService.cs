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
    public abstract class BaseService<TModel, TCustomRepository> : IBaseService<TModel> where TCustomRepository : IBaseRepository<TModel>
    {
        public readonly TCustomRepository _repository;

        public BaseService(TCustomRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task CreateAsync(TModel entity)
        {
            await _repository.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<TModel?> GetByIdAsync(string id)
        {
            var item = await _repository.GetByIdAsync(id);

            return item;
        }

        public virtual async Task<PaginationResponse<TModel>> GetPaginationAsync(int pageNumber, int perPage)
        {
            var (itens, totalResults) = await _repository.GetAllAsync(pageNumber, perPage);

            var response = new PaginationResponse<TModel>(itens, totalResults, pageNumber, perPage);

            return response;
        }

        public virtual async Task UpdateAsync(TModel entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
