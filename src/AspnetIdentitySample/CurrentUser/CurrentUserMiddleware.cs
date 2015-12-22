using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using AspnetIdentitySample.Models;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetIdentitySample.CurrentUser
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ICurrentUserService cus, UserManager<ApplicationUser> um)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                ApplicationUser au = await um.FindByIdAsync(httpContext.User.GetUserId());
                await cus.Set(au);
            }
            await _next(httpContext);
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
