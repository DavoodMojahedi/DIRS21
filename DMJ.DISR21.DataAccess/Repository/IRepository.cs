using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace DMJ.DIRS21.DataAccess.Repository
{
    public interface IRepository
    {
        Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new();

        IMongoQueryable<TEntity> All<TEntity>() where TEntity : class, new();
        
        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new();
        
        Task InsertOneAsync<TEntity>(TEntity item) where TEntity : class, new();
        
        Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new();
        
        Task<long> DeleteAsync<TEntity>(string id) where TEntity : class, new();
        
        Task<long> DeleteAsync<TEntity>(ObjectId objectId) where TEntity : class, new();

        Task UpdateOneAsync<TEntity>(TEntity item, Expression<Func<TEntity, bool>> filter) where TEntity : class, new();
        
        Task UpdateOneAsync<TEntity>(TEntity item, string id) where TEntity : class, new();
        
        Task UpdateOneAsync<TEntity>(TEntity item, ObjectId objectId) where TEntity : class, new();

    }
}
