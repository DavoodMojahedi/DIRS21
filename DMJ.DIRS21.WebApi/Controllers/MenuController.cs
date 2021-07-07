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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost("OrderMenu")]
        public async Task<ResultViewModel<bool>> OrderMenuAsync(MenuInsertVm menu)
        {
            int h = 0;

            var jj = 12 / h;
            var result = await _menuService.OrderMenuAsync(menu);

            if (result.ToLower() == "ok")
                return ControllerTools.CreateSuccessResult(true);

            return new ResultViewModel<bool>()
            {
                ErrorModel = new List<string>() {result}
            };
        }

    }
}
