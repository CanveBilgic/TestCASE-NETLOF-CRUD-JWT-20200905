using CORE.Models;
using WebAPI.ViewModels;
using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using System.Net;
using CORE.Utilities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<string>), 400)]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager,
                                ITokenService tokenService)
            : base (userManager)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
        }


        // TODO: NLog thinks that |Authorization was successful for user: (null)|
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> SignIn([FromBody] AuthenticateModel model)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            User user = null;
            bool result = false;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                    return BadRequest("Wrong username or password");

                result = await _userManager.CheckPasswordAsync(user, model.Password);
                //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false)
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex, this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
                httpStatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)httpStatusCode);
            }
            if (!result)
                return BadRequest("Wrong email or password");
            else
                return Ok(_tokenService.Generate(user));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> SignUp([FromBody] CreateNewUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = new User
            {
                UserName = model.Name
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
                return BadRequest("Sorry, an unexpected error occured");

            return Ok("Your user has been created");
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult ExtendToken()
        {
            return Ok(_tokenService.Generate(CurrentUser));
        }

    }
}