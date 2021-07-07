using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("InsertRestaurant")]
        public async Task InsertRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantService.InsertAsync(restaurant);

        }

        [HttpPost("UpdateRestaurant")]
        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantService.UpdateAsync(restaurant);

        }

    }
}
