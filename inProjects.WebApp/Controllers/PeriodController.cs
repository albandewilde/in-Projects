using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.TimePeriod;
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
    public class PeriodController : Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public PeriodController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
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

                if(await aclQueries.VerifyGrantLevelByUserId(112,9,userId, Operator.SuperiorOrEqual ) == false )
                {
                    Result result = new Result( Status.BadRequest, "Vous n'etes pas autorisé à utiliser cette fonctionnalité !" );
                    return this.CreateResult( result);
                }

                var idZone = await timePeriod.CreateTimePeriodAsync( ctx, 1, createPeriodModel.begDate, createPeriodModel.endDate, createPeriodModel.Kind, createPeriodModel.idZone );
                if(idZone == 0 )
                {
                    Result result = new Result( Status.BadRequest, "Quelques choses s'est mal passé durant la création de periode." );
                    return this.CreateResult( result );
                }

                for( int i = 0; i < createPeriodModel.Groups.Count; i++ )
                {
                    int idGroup;

                    if( createPeriodModel.Groups[i].IsAlreadyPermanent == false && createPeriodModel.Groups[i].State == true )
                    {
                        idGroup = await group.CreateGroupAsync( ctx, userId, 4 );
                        await group.Naming.GroupRenameAsync( ctx, 17, idGroup, createPeriodModel.Groups[i].Name );
                    }

                    idGroup = await group.CreateGroupAsync( ctx, userId, idZone );
                    await group.Naming.GroupRenameAsync( ctx, 17, idGroup, createPeriodModel.Groups[i].Name );
                }

                return this.CreateResult( Result.Success() );

            }
        }

    }
}
