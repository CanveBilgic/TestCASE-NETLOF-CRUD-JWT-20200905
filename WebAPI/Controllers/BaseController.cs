using CORE.Entities;
using CORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private User _user;
        public User CurrentUser => GetCurrentUser();

        public BaseController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        private User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            if (_user != null)
                return _user;

            var userId = User?.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            _user = _userManager.FindByIdAsync(userId).Result;
            return _user;
        }
    }
}