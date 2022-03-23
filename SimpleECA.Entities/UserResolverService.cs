using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleECA.Entities
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUser()
        {
            return _context.HttpContext.User?.Identity?.Name;
            //var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            //return (claim != null) ? claim.Value : "0";
        }
    }
}
