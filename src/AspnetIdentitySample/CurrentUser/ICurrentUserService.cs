using AspnetIdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetIdentitySample.CurrentUser
{
    public interface ICurrentUserService
    {
        Task<bool> Set(ApplicationUser au);
        UserDetails Get();
    }
}
