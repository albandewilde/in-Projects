using CK.Core;
using CK.SqlServer;
using inProjects.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    public class AdministatorController : Controller
    {
        readonly IStObjMap _stObjMap;

        public AdministatorController( IStObjMap stObjMap )
        {
            _stObjMap = stObjMap;
        }

        [HttpPost( "createPeriod" )]
        public async Task<IActionResult> CreatePeriod( [FromBody] CreatePeriodModel createPeriodModel )
        {
            throw new NotImplementedException();

            //using( var ctx = new SqlStandardCallContext() )
            //{
            //}
        }

    }
}
