using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleECA.Helpers.Enums;
using SimpleECA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Helpers.Authentication
{
    public static class ClaimsHelper
    {
        public static void AppAuthetication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie();
        }
        public static Task DoLogin(HttpContext httpContext, AuthUserViewModel user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimType.UserName, user.FullName),
                    new Claim(ClaimType.UserId, user.Id + ""),
                    new Claim(ClaimType.RoleId, user.RoleId + ""),
                    new Claim(ClaimType.RoleName, user.RoleName + "")
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>, 
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),                
                //IsPersistent = true,
                //IssuedUtc = <DateTimeOffset>,  
                //RedirectUri = <string>
            };
            return httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
        }

        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : "0";
        }
    }
}
