using System.Threading.Tasks;
using DMJ.DIRS21.Model.ClassVm;
using DMJ.DIRS21.Model.Documents;

namespace DMJ.DIRS21.Service.Contracts
{
    public interface IMenuService
    {
        Task<string> OrderMenuAsync(MenuInsertVm insertVm);
        
        Task UpdateAsync(Menu menu);

        Task<long> DeleteAsync(Menu menu);
        
    }
}
