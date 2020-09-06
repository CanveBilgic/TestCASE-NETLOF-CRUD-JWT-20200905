using WebAPI.Models;
using WebAPI.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebAPI.Services.Interfaces
{
    public interface IMaintenanceService
    {
         Task<ProcessResult> CreateOrUpdateAsync(MaintenanceNewModel model);
         Task<ProcessResult> RemoveOrRestoreAsync(int id);
    }
}