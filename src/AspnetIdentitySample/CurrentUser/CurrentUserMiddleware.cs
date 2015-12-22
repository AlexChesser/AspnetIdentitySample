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

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                // MiddleWare is only instantiated once for the lifecycle of the project.
                // as such, IServices cannot be injected if you are hoping to retrieve SCOPED
                // services.
                // There is however, an iserviceprovider attached to each request which can be
                // used to resolve scoped dependencies within middleware.
                // Per Kiran Challa's answer on Stack overflow
                // http://stackoverflow.com/a/34406675/398055
                IServiceProvider sp = httpContext.RequestServices;
                ICurrentUserService _cu = sp.GetRequiredService<ICurrentUserService>();;
                UserManager<ApplicationUser> _um = sp.GetRequiredService<UserManager<ApplicationUser>>();
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
