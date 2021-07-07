using System.Threading.Tasks;
using DMJ.DIRS21.Model.ClassVm;
using DMJ.DIRS21.Model.Documents;

namespace DMJ.DIRS21.Service.Contracts
{
    public interface IRestaurantService
    {
        Task InsertAsync(Restaurant restaurant);
        
        Task UpdateAsync(Restaurant restaurant);

        Task<long> DeleteAsync(Restaurant restaurant);
        
        Task<long> DeleteByCodeAsync(int code);

    }
}
