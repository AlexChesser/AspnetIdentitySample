using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using AspnetIdentitySample.CurrentUser;
using AspnetIdentitySample.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetIdentitySample.API
{
    [Route("api/[controller]")]
    public class CurrentUserController : Controller
    {
        private ICurrentUserService _cus;
        public CurrentUserController(ICurrentUserService cus) {
            _cus = cus;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            UserDetails ud = _cus.Get();
            return Ok(new JsonResult(ud));
        }

    }
}
