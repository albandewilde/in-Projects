using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Event;
using inProjects.Data.Data.Group;
using inProjects.Data.Data.TimedUser;
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
    public class EventController : Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public EventController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDataBase );
                EventQueries eventQueries = new EventQueries( ctx, sqlDataBase );

                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );

                AclQueries aclQueries = new AclQueries( ctx, sqlDataBase );

                if( !await aclQueries.VerifyGrantLevelByUserId( 112, await aclQueries.GetAclIdBySchoolId( groupData.ParentZoneId), userId, Operator.SuperiorOrEqual ))
                {
                    Result result = new Result( Status.Unauthorized, "Vous n'etes pas autorisé à utiliser cette fonctionnalité !" );
                    return this.CreateResult( result );
                }

                List<EventData> events = await eventQueries.GetAllEventsByIdSchool( groupData.ParentZoneId );

                return Ok( events );


            }
        }

        [HttpPost("UpdateEvents")]
        public async Task<IActionResult> UpdateEvents([FromBody] SchoolEventChangeModel model)
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDataBase );
                EventQueries eventQueries = new EventQueries( ctx, sqlDataBase );

                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );

                AclQueries aclQueries = new AclQueries( ctx, sqlDataBase );

                if( !await aclQueries.VerifyGrantLevelByUserId( 112, await aclQueries.GetAclIdBySchoolId( groupData.ParentZoneId ), userId, Operator.SuperiorOrEqual ) )
                {
                    Result result = new Result( Status.Unauthorized, "Vous n'etes pas autorisé à utiliser cette fonctionnalité !" );
                    return this.CreateResult( result );
                }

                await eventQueries.UpdateEvent( model.EventId, model.Name, model.BegDate, model.EndDate );

                List<EventData> events = await eventQueries.GetAllEventsByIdSchool( groupData.ParentZoneId );

                return Ok( events );


            }
        }

        [HttpPost( "CreateEvents" )]
        public async Task<IActionResult> CreateEvents( [FromBody] SchoolEventChangeModel model )
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            var eventTable = _stObjMap.StObjs.Obtain<EventSchoolTable>();

            using( var ctx = new SqlStandardCallContext() )
            {
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDataBase );
                TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries( ctx, sqlDataBase );
                EventQueries eventQueries = new EventQueries( ctx, sqlDataBase );

                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );

                AclQueries aclQueries = new AclQueries( ctx, sqlDataBase );

                if( !await aclQueries.VerifyGrantLevelByUserId( 112, await aclQueries.GetAclIdBySchoolId( groupData.ParentZoneId ), userId, Operator.SuperiorOrEqual ) )
                {
                    Result result = new Result( Status.Unauthorized, "Vous n'etes pas autorisé à utiliser cette fonctionnalité !" );
                    return this.CreateResult( result );
                }

                if( !model.IsOther )
                {

                    model.Name += "-" + await timedPeriodQueries.GetGroupNameActualPeriod( groupData.ZoneId, groupData.ParentZoneId );
                }

                await eventTable.CreateEvent( ctx, model.Name, model.BegDate, model.EndDate, groupData.ZoneId, userId );
                List<EventData> events = await eventQueries.GetAllEventsByIdSchool( groupData.ParentZoneId );

                return Ok( events );


            }
        }

        //public async Task<bool> IsUserHasRight( SqlStandardCallContext ctx, SqlDefaultDatabase sqlDataBase,int userId  )
        //{
        //    GroupQueries groupQueries = new GroupQueries( ctx, sqlDataBase );
        //    EventQueries eventQueries = new EventQueries( ctx, sqlDataBase );

        //    GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );

        //    AclQueries aclQueries = new AclQueries( ctx, sqlDataBase );

        //    if( !await aclQueries.VerifyGrantLevelByUserId( 112, await aclQueries.GetAclIdBySchoolId( groupData.ParentZoneId ), userId, Operator.SuperiorOrEqual ) )
        //    {
        //        return false;
        //    }else
        //    {
        //        return true;
        //    }
        //}
    }
}
