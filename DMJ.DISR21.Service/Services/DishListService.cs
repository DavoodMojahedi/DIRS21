using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ.DIRS21.DataAccess.Repository;
using DMJ.DIRS21.Model;
using DMJ.DIRS21.Model.Documents;
using DMJ.DIRS21.Model.SearchVms;
using DMJ.DIRS21.Service.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DMJ.DIRS21.Service.Services
{
    public class DishListService : IDishListService
    {
        private readonly IRepository _repository;
        private IMongoQueryable<Dish> _query;
        private DishSearchVm _dishSearchVm;
        public DishListService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Dish>> GetActiveDishesAsync(DishSearchVm dishSearchVm)
        {
            _dishSearchVm.ActivityStatus = true;

            return await GetDishesAsync(dishSearchVm);
        }

        public async Task<IEnumerable<Dish>> GetInactiveDishesAsync(DishSearchVm dishSearchVm)
        {
            _dishSearchVm.ActivityStatus = false;

            return await GetDishesAsync(dishSearchVm);
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync(DishSearchVm dishSearchVm)
        {
            _dishSearchVm = dishSearchVm;

            _query = _repository.All<Dish>();

            ApplyFilters();

            return await _query.ToListAsync();
        }

        private void ApplyFilters()
        {
            ApplyActivityStatusFilter();
            ApplyDishCodesFilter();
            ApplyRestaurantCodeFilter();
            ApplyTimeInDayAvailabilityFilter();
            ApplyDayInWeekAvailabilityFilter();
        }

        public void ApplyDishCodesFilter()
        {
            if (_dishSearchVm.DishCodes!=null && !_dishSearchVm.DishCodes.Any())
                return;

            _query = _query
                .Where(c => _dishSearchVm.DishCodes.Contains(c.Code));
        }

        public void ApplyActivityStatusFilter()
        {
            if (!_dishSearchVm.ActivityStatus.HasValue)
                return;

            _query = _query
                .Where(c => c.IsActive == _dishSearchVm.ActivityStatus);
        }

        public void ApplyRestaurantCodeFilter()
        {
            if (!_dishSearchVm.RestaurantCode.HasValue)
                return;

            _query = _query
                .Where(c => c.RestaurantCode == _dishSearchVm.RestaurantCode);
        }

        public void ApplyTimeInDayAvailabilityFilter()
        {
            if (!_dishSearchVm.TimeInDayAvailability.HasValue)
                return;

            _query = _query
                .Where(c => c.DishAvailabilityInDay == _dishSearchVm.TimeInDayAvailability);
        }

        public void ApplyDayInWeekAvailabilityFilter()
        {
            if (!_dishSearchVm.DayInWeekAvailability.HasValue)
                return;

            _query = _query
                .Where(c => c.DishAvailabilityInWeek == _dishSearchVm.DayInWeekAvailability);
        }
        
    }
}
