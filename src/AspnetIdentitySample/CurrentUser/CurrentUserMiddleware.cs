using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using AspnetIdentitySample.Models;
using System.Security.Claims;

namespace AspnetIdentitySample.CurrentUser
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;
        private ICurrentUserService _cu;
        private UserManager<ApplicationUser> _um;

        public CurrentUserMiddleware(RequestDelegate next,
            UserManager<ApplicationUser> um,
            ICurrentUserService cu)
        {
            _cu = cu;
            _um = um;
            _next = next;
        }

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                ApplicationUser au = await _um.FindByIdAsync(httpContext.User.GetUserId());
                await _cu.Set(au);
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CurrentUserExtensions
    {
        public static IApplicationBuilder SetCurrentUser(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CurrentUserMiddleware>();
        }
    }
}
