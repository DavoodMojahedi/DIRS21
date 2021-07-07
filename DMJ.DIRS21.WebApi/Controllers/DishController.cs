using System.Collections.Generic;
using System.Threading.Tasks;
using DMJ.DIRS21.Model;
using DMJ.DIRS21.Model.ClassVm;
using DMJ.DIRS21.Model.Documents;
using DMJ.DIRS21.Model.SearchVms;
using DMJ.DIRS21.Service.Contracts;
using DMJ.DIRS21.WebApi.Tools;
using Microsoft.AspNetCore.Mvc;

namespace DMJ.DIRS21.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishListService _dishListService;
        private readonly IDishChangeService _dishChangeService;

        public DishController(IDishListService dishListService, 
            IDishChangeService dishChangeService)
        {
            _dishListService = dishListService;
            _dishChangeService = dishChangeService;
        }

        [HttpPost("GetRestaurantActiveDishes")]
        public async Task<ResultViewModel<IEnumerable<Dish>>> GetRestaurantActiveDishesAsync(DishSearchVm menuSearchVm)
        {
            return ControllerTools.CreateSuccessResult(
                await _dishListService.GetActiveDishesAsync(menuSearchVm));
        }

        [HttpPost("GetRestaurantInactiveDishesAsync")]
        public async Task<ResultViewModel<IEnumerable<Dish>>> GetRestaurantInactiveDishesAsync(DishSearchVm menuSearchVm)
        {
            return ControllerTools.CreateSuccessResult(
                await _dishListService.GetInactiveDishesAsync(menuSearchVm));
        }

        [HttpPost("GetDishes")]
        public async Task<ResultViewModel<IEnumerable<Dish>>> GetDishesAsync(DishSearchVm menuSearchVm)
        {
            return ControllerTools.CreateSuccessResult(
                await _dishListService.GetDishesAsync(menuSearchVm));
        }

        [HttpPost("InsertDish")]
        public async Task InsertDishAsync(Dish dish)
        {
            await _dishChangeService.InsertDishAsync(dish);
        }

        [HttpDelete]
        public async Task DeleteDishByCodeAsync(int code)
        {
            await _dishChangeService.DeleteDishByCodeAsync(code);
        }

        [HttpPost("AddComment")]
        public async Task AddCommentAsync(DishCommentVm comments)
        {
            await _dishChangeService.AddCommentsAsync(comments);
        }

        [HttpPost("AddScore")]
        public async Task AddScoreAsync(DishScoreVm scoreVm)
        {
            await _dishChangeService.AddScoreAsync(scoreVm);
        }

        
    }
}
