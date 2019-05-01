using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Queries;
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
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            var timePeriod = _stObjMap.StObjs.Obtain<TimePeriodTable>();

            using( var ctx = new SqlStandardCallContext() )
            {
                AclQueries aclQueries = new AclQueries( ctx, sqlDatabase );

                if(await aclQueries.VerifySuperiorOrEqualGrantLevelUserByUserId(127,9,createPeriodModel.idUser) == false )
                {
                    Result result = new Result( Status.BadRequest, "You are not allowed to do this action" );
                    return this.CreateResult(result);
                }

                var id = await timePeriod.CreateTimePeriodAsync( ctx, 1, createPeriodModel.begDate, createPeriodModel.endDate, createPeriodModel.Kind.ToString(), createPeriodModel.idZone );
                if(id == 0 )
                {
                    Result result = new Result( Status.BadRequest, "Something wrong has occured while the creation of period" );
                    return this.CreateResult( result );
                }

                return this.CreateResult( Result.Success() );

            }
        }

    }
}
