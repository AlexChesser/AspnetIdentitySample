using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetIdentitySample.Models;
using Microsoft.Data.Entity;
using System.Diagnostics;

namespace AspnetIdentitySample.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private ApplicationDbContext _ctx;
        private ApplicationUser _au;
        private UserDetails _ud;

        public CurrentUserService(ApplicationDbContext ctx) {
            _ctx = ctx;
        }

        public UserDetails Get()
        {
            return _ud;
        }

        public async Task<bool> Set(ApplicationUser au)
        {
            _au = au;
            _ud = await _ctx.UserDetails
                .Where(ud => ud.UserDetailsID == au.UserDetailsID)
                .FirstOrDefaultAsync();
            Debug.WriteLine(_ud.PublicInformation);
            return true;
        }
    }
}
