using Microsoft.AspNetCore.Mvc;
using SimpleECA.Helpers.Authentication;
using SimpleECA.Helpers.Enums;
using SimpleECA.Models;
using SimpleECA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> SignUp(UserDetailsViewModel user)
        {
            var res = await _userService.CreateUser(user);
            return Ok(res);
        }
        public IActionResult CreateUserAddress()
        {
            return PartialView("_CreateUserAddressPartial");
        }
        public async Task<IActionResult> GetUserAddress()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _userService.GetUserAddressList(userid);
            return PartialView("_UserAddressListPartial", res);
        }
        public async Task<IActionResult> SaveUserAddress(UserAddressViewModel model)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            model.userid = userid;
            var res = await _userService.CreateUserAddress(model);
            return Ok(res);
        }
    }
}
