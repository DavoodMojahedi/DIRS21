using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DMJ.DIRS21.Model.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Linq;

namespace DMJ.DIRS21.DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly IMongoDatabase _db;
        private MongoDbConfiguration _mongoDbConfiguration;

        public Repository(IOptions<MongoDbConfiguration> mongoDbConfigurationAccessor)
        {
            var mongoDbConfiguration = mongoDbConfigurationAccessor.Value;

            var client = new MongoClient(mongoDbConfiguration.Connection);

            _db = client.GetDatabase(mongoDbConfiguration.Database);
        }

        public async Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entity = await collection.AsQueryable()
                .Where(filter)
                .SingleOrDefaultAsync();

            return entity;
        }

        public IMongoQueryable<TEntity> All<TEntity>() where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();

            return collection.AsQueryable();
        }

        public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();

            return await collection.AsQueryable()
                .AnyAsync(filter);
        }

        public async Task InsertOneAsync<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            
            await collection.InsertOneAsync(item);
        }

        public async Task UpdateOneAsync<TEntity>(TEntity item,
            Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();

            await collection.ReplaceOneAsync(filter, item);
        }

        public async Task UpdateOneAsync<TEntity>(TEntity item,
            string id) where TEntity : class, new()
        {
            var filter = Builders<TEntity>.Filter.Eq("id", ObjectId.Parse(id));

            var collection = GetCollection<TEntity>();

            await collection.ReplaceOneAsync(filter, item);
        }

        public async Task UpdateOneAsync<TEntity>(TEntity item,
            ObjectId objectId) where TEntity : class, new()
        {
            var filter = Builders<TEntity>.Filter.Eq("id", objectId);

            var collection = GetCollection<TEntity>();

            await collection.ReplaceOneAsync(filter, item);
        }

        public async Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();

            var deleteResult = await collection.DeleteOneAsync(filter: filter);

            return deleteResult.DeletedCount;
        }

        public async Task<long> DeleteAsync<TEntity>(string id) where TEntity : class, new()
        {
            var filter = Builders<TEntity>.Filter.Eq("id", ObjectId.Parse(id));
        
            var collection = GetCollection<TEntity>();

            var deleteResult = await collection.DeleteOneAsync(filter: filter);

            return deleteResult.DeletedCount;
        }

        public async Task<long> DeleteAsync<TEntity>(ObjectId objectId) where TEntity : class, new()
        {
            var filter = Builders<TEntity>.Filter.Eq("id", objectId);

            var collection = GetCollection<TEntity>();

            var deleteResult = await collection.DeleteOneAsync(filter: filter);

            return deleteResult.DeletedCount;
        }

        private IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return  _db.GetCollection< TEntity>(typeof(TEntity).Name);
        }
    }
}
