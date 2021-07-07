using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DMJ.DIRS21.DataAccess.Repository;
using DMJ.DIRS21.Model;
using DMJ.DIRS21.Model.ClassVm;
using DMJ.DIRS21.Model.Documents;
using DMJ.DIRS21.Model.SearchVms;
using DMJ.DIRS21.Service.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DMJ.DIRS21.Service.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository _repository;
        public RestaurantService(IRepository repository)
        { 
            _repository = repository;
        }

        public async Task InsertAsync(Restaurant restaurant)
        {
            await _repository.InsertOneAsync(restaurant);
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            await _repository.UpdateOneAsync(restaurant, c => c.Code == restaurant.Code);
        }

        public async Task<long> DeleteAsync(Restaurant restaurant)
        {
            return await _repository.DeleteAsync<Restaurant>(c => c.Code == restaurant.Code);
        }


        public async Task<long> DeleteByCodeAsync(int code)
        {
            return await _repository.DeleteAsync<Restaurant>(c => c.Code == code);
        }
    }
}
