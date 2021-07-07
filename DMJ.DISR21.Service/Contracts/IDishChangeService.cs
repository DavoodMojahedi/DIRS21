using System.Collections.Generic;
using System.Threading.Tasks;
using DMJ.DIRS21.DataAccess.Repository;
using DMJ.DIRS21.Model;
using DMJ.DIRS21.Model.ClassVm;
using DMJ.DIRS21.Model.Documents;
using DMJ.DIRS21.Model.SearchVms;

namespace DMJ.DIRS21.Service.Contracts
{
    public interface IDishChangeService
    {
        Task InsertDishAsync(Dish dish);
        
        Task UpdateDishAsync(Dish dish);

        Task<long> DeleteDishAsync(Dish dish);
        
        Task<long> DeleteDishByCodeAsync(int code);

        Task AddCommentsAsync(DishCommentVm commentVm);

        Task AddScoreAsync(DishScoreVm scoreVm);

        Task DeactivateDishAsync(DishDeactivationVm deactivationVm);

        Task ActivateDishAsync(int dishCode);


    }
}
