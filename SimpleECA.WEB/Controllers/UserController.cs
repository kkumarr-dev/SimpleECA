using Microsoft.AspNetCore.Mvc;
using SimpleECA.Models.UserViewModel;
using SimpleECA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleECA.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDetailsViewModel user)
        {
            var res = await _userService.CreateUser(user);
            return Ok(res);
        }
    }
}
