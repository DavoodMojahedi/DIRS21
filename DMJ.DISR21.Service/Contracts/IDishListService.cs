using System.Collections.Generic;
using System.Threading.Tasks;
using DMJ.DIRS21.DataAccess.Repository;
using DMJ.DIRS21.Model;
using DMJ.DIRS21.Model.Documents;
using DMJ.DIRS21.Model.SearchVms;

namespace DMJ.DIRS21.Service.Contracts
{
    public interface IDishListService
    {
        Task<IEnumerable<Dish>> GetActiveDishesAsync(DishSearchVm menuSearchVm);
        
        Task<IEnumerable<Dish>> GetInactiveDishesAsync(DishSearchVm menuSearchVm);

        Task<IEnumerable<Dish>> GetDishesAsync(DishSearchVm dishSearchVm);


    }
}
