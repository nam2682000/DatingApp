using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Application
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(string id);
        Task CreateAsync(T entity);
        Task CreateManyAsync(List<T> entitys);
        Task<bool> UpdateAsync(string id, T entity);
        Task<bool> DeleteManyAsync(string[] ids);
        Task<List<T>> GetWhereSelectAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null);
        Task<T> FindWhereAsync(FilterDefinition<T> filter);
        Task<bool> AnyAsync(FilterDefinition<T> filter);
    }
}