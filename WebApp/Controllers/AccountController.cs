using inProjects.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly IOptions<SpaOptions> _spaOptions;

        public AccountController( IOptions<SpaOptions> spaOptions )
        {
            _spaOptions = spaOptions;
        }

        [HttpPost]
        [AllowAnonymous]
        public void Register(RegisterModel model)
        {
            
        }
    }
}
