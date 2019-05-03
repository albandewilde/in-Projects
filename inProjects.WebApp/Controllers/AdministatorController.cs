using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Queries;
using inProjects.ViewModels;
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
        readonly IAuthenticationInfo _authenticationInfo;

        public AdministatorController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }

        [HttpPost( "createPeriod" )]
        public async Task<IActionResult> CreatePeriod( [FromBody] CreatePeriodModel createPeriodModel )
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            userId = 20;
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            var group = _stObjMap.StObjs.Obtain<CustomGroupTable>();
            var timePeriod = _stObjMap.StObjs.Obtain<TimePeriodTable>();

            using( var ctx = new SqlStandardCallContext() )
            {
                AclQueries aclQueries = new AclQueries( ctx, sqlDatabase );

                if(await aclQueries.VerifySuperiorOrEqualGrantLevelUserByUserId(112,9,userId) == false )
                {
                    Result result = new Result( Status.BadRequest, "You are not allowed to do this action" );
                    //Result resTest = new Result<CreatePeriodModel>( Status.Ok, createPeriodModel, "lol" );
                    var test = this.CreateResult( result );
                    return test ;
                }

                var idZone = await timePeriod.CreateTimePeriodAsync( ctx, 1, createPeriodModel.begDate, createPeriodModel.endDate, createPeriodModel.Kind, createPeriodModel.idZone );
                if(idZone == 0 )
                {
                    Result result = new Result( Status.BadRequest, "Something wrong has occured while the creation of period" );
                    return this.CreateResult( result );
                }

                for( int i = 0; i < createPeriodModel.Groups.Count; i++ )
                {
                    int idGroup = await group.CreateGroupAsync( ctx, userId, idZone );
                    await group.Naming.GroupRenameAsync( ctx, 17, idGroup, createPeriodModel.Groups[i] );
                }

                return this.CreateResult( Result.Success() );

            }
        }

    }
}
