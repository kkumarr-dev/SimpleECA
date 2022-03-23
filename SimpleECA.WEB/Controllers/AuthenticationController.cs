using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleECA.Helpers.Authentication;
using SimpleECA.Models;
using SimpleECA.Models.UserViewModel;
using SimpleECA.Services;
using SimpleECA.WEB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleECA.WEB.Controllers
{
    [AllowAnonymous, Route("u")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private IHttpContextAccessor _accessor;
        private readonly IUserService _userService;
        public AuthenticationController(IAuthService authService, IHttpContextAccessor accessor, IUserService userService)
        {
            _authService = authService;
            _accessor = accessor;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("u-register")]
        public async Task<IActionResult> Register(UserDetailsViewModel user)
        {
            var res = await _authService.CreateUser(user);
            return Ok(res);
        }
        [Route("u-login")]
        public async Task<IActionResult> Login(AuthenticateRequestViewModel model)
        {
            var response = await _authService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        [Route("g-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") }; //"Index", "Home"
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("g-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims;

            var userData = new AuthUserViewModel();

            userData.FullName = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            userData.Email = claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            userData.Username = claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            userData.Password = claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            userData.RoleId = 2;
            if (claims.Any(x => x.Type == ClaimTypes.MobilePhone))
            {
                userData.PhoneNumber = claims.Where(x => x.Type == ClaimTypes.MobilePhone).FirstOrDefault().Value;
            }
            else
            {
                userData.PhoneNumber = "";
            }

            await ClaimsHelper.DoLogin(HttpContext, userData);

            var createUser = new UserDetailsViewModel
            {
                email = userData.Email,
                firstname = userData.FullName,
                lastname = "",
                isactive = true,
                mobilenumber = userData.PhoneNumber,
                userroleid = 4,
                rpassword = userData.Password
            };

            var res = await _userService.CreateUser(createUser);

            return RedirectToAction("Index", "Home");
        }
        [Route("f-login")]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [Route("f-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);
        }

        public async Task<IActionResult> GetAll()
        {
            var res = await _authService.GetAll();
            return Ok(res);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Authentication");
        }
    }
}
