using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using CORE.Entities;
using CORE.Backend;
using WebAPI.Models;
using WebAPI.Services.Interfaces;
using WebAPI.ViewModels;
using CORE.Utilities;

namespace WebAPI.Services
{
    public class MaintenanceService : BaseService, IMaintenanceService
    {
        public MaintenanceService(UserManager<User> userManager,
                              IHttpContextAccessor contextAccessor,
                              DbOperator dbOperator)
            : base(userManager, contextAccessor, dbOperator)
        {
        }

        public async Task<ProcessResult> CreateOrUpdateAsync(MaintenanceNewModel model)
        {
            bool isCreated = false;
            Func<Task> action = async () =>
            {
                try
                {
                    var maintenance = await dbOperator.Maintenances.Where(x => x.ID == model.ID).SingleOrDefaultAsync();
                    if (maintenance == null)
                    {
                        isCreated = true;
                        maintenance = new Maintenance();
                    }
                    else
                    {
                        if (CurrentUser.Id != maintenance.Status.CreatedBy)
                            throw new InvalidOperationException("Yalnızca arızanın sahibi güncelleme yapabilmektedir.");
                    }

                    maintenance.Status = PrepareStatusBase(maintenance.Status == null ? new StatusBase() : maintenance.Status, isCreated);
                    maintenance.Description = model.Description;
                    await dbOperator.AddAsync(maintenance);
                    await dbOperator.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Utilities.LogError(ex, this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            };
            return await Process.RunAsync(action);
        }


        public async Task<ProcessResult> RemoveOrRestoreAsync(int id)
        {
            Func<Task> action = async () =>
            {
                var maintenance = await dbOperator.Maintenances.Where(x => x.ID == id).SingleAsync();
                if (maintenance.UserID != CurrentUser.Id)
                    throw new InvalidOperationException("Only creator can remove/restore the comment");

                maintenance.Status.IsDeleted = !maintenance.Status.IsDeleted;
                maintenance.Status.ModifyDate = DateTime.Now;
                maintenance.Status.ModifiedBy = CurrentUser.Id;
                await dbOperator.SaveChangesAsync();
            };

            return await Process.RunAsync(action);
        }
    }
}