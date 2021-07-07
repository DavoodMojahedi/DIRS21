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
    public class MenuService : IMenuService
    {
        private readonly IRepository _repository;
        private readonly IDishListService _dishListService;
        public MenuService(IRepository repository, IDishListService dishListService)
        {
            _repository = repository;
            _dishListService = dishListService;
        }

        public async Task<string> OrderMenuAsync(MenuInsertVm menu)
        {
            var restaurant = await _repository
                .SingleAsync<Restaurant>(c => c.Code == menu.RestaurantCode);

            if (restaurant == null) return "Restaurant is not available";

            var dishes = (await _dishListService.GetDishesAsync(
                new DishSearchVm()
                {
                    DishCodes = menu.DishCodes
                }))
                .ToList();
            
            if (!dishes.Any()) return "dishes are not available";

            if (dishes.Any(c => c.IsActive)) return "At least one of dishes in menu is inactive";

            var toBeInsertedMenu = new Menu()
            {
                Name = menu.Name,
                OrderDate = DateTime.Now,
                Restaurant = restaurant,
                Dishes = dishes.ToList()
            };

            await _repository.InsertOneAsync(toBeInsertedMenu);

            return "OK";
        }

        public async Task UpdateAsync(Menu menu)
        {
            await _repository.UpdateOneAsync(menu, menu._id);
        }

        public async Task<long> DeleteAsync(Menu menu)
        {
            return await _repository.DeleteAsync<Menu>(menu._id);
        }

    }
}
