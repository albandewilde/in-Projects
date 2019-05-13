using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Data.Period;
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
    public class GroupController : Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public GroupController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }

        [HttpGet("getAllGroupTemplate")]
        public async Task<List<GroupPeriod>> GetAllGroupTemplate()
        {
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDataBase );

                List<string> listGroups = await groupQueries.GetAllGroupByZoneId( 4 );

                List<GroupPeriod> list = new List<GroupPeriod>();

                for( int i = 0; i < listGroups.Count; i++ )
                {
                    GroupPeriod gp = new GroupPeriod();
                    gp.Name = listGroups[i];
                    gp.State = true;
                    gp.IsAlreadyPermanent = true;
                    list.Add( gp );
                }

                return list;
            }
        }
        [HttpGet( "getGroupUserAccessPanel" )]
        public async Task<string> GetGroupUserAccessPanel(int idZone)
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            if( userId == 0 ) return "Anon";

            using( var ctx = new SqlStandardCallContext() )
            {
                TimedPeriodQueries timedPeriod = new TimedPeriodQueries( ctx, sqlDataBase );
                TimedUserQueries timedUser = new TimedUserQueries( ctx, sqlDataBase );
                GroupQueries groupsQueries = new GroupQueries( ctx, sqlDataBase );

                PeriodData period = await timedPeriod.GetLastPeriodBySchool( idZone );
                TimedUserData timedUserData = await timedUser.GetTimedUser( userId, period.ChildId );

                if(timedUserData == null )
                {
                    return "Anon";
                }

                string whatTimed = await timedUser.getWhichCat( timedUserData.TimedUserId );
                if(whatTimed != "StaffMember")
                {
                    return whatTimed;
                }

                List<string> listGroupsOfTimedUser = new List<string>();

                listGroupsOfTimedUser = await groupsQueries.GetAllGroupOfTimedUser( period.ChildId, timedUserData.TimedUserId );

                for( int i = 0; i < listGroupsOfTimedUser.Count; i++ )
                {
                    if(listGroupsOfTimedUser[i] == "Administration" )
                    {
                        return "Administration";
                    }
                    else if(listGroupsOfTimedUser[i] == "Teacher" )
                    {
                        return "Teacher";
                    }
                    
                }

                return "User";
            }
        }
    }
}
