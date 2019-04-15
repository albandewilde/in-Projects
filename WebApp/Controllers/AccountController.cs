using CK.AspNet.Auth;
using CK.Auth;
using inProjects.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly IOptions<SpaOptions> _spaOptions;
        readonly IAuthenticationTypeSystem _typeSystem;

        public AccountController( IOptions<SpaOptions> spaOptions, IAuthenticationTypeSystem typeSystem )
        {
            _spaOptions = spaOptions;
            _typeSystem = typeSystem;
        }

        [HttpPost]
        [AllowAnonymous]
        public void Register( [FromBody]RegisterModel model )
        {

        }
    }
}
