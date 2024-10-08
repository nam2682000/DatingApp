using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public RepositoryBase(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T> FindByIdAsync(string id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync(FilterDefinition<T> filter)
        {
            return await _collection.Find(filter).AnyAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<List<T>> GetWhereSelectAsync(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null)
        {
            if(projection is null){
                return await _collection.Find(filter).ToListAsync();
            }
            return await _collection.Find(filter).Project<T>(projection).ToListAsync();
        }

        public async Task<T> FindWhereAsync(FilterDefinition<T> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateManyAsync(List<T> entitys)
        {
            await _collection.InsertManyAsync(entitys);
        }

        public async Task<bool> DeleteManyAsync(string[] ids)
        {
            var filter = Builders<T>.Filter.In("_id", ids);
            var result = await _collection.DeleteManyAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}