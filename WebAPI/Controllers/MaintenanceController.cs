using CORE.Models;
using WebAPI.ViewModels;
using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using CORE.Entities;
using WebAPI.Models;
using System;
using CORE.Utilities;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<string>), 400)]
    public class MaintenanceController : BaseController
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(UserManager<User> userManager,
                                IMaintenanceService maintenanceService,
                                ILogger<MaintenanceController> logger)
            : base(userManager)
        {
            this._maintenanceService = maintenanceService;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] MaintenanceNewModel model)
        {
            ProcessResult result = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                result = await _maintenanceService.CreateOrUpdateAsync(model);
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex, this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
                httpStatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)httpStatusCode);
            }
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOrRestore(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _maintenanceService.RemoveOrRestoreAsync(id);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

    }
}