using CK.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using CK.SqlServer.Setup;
using System.Threading.Tasks;
using inProjects.Data.Data.TimedUser;

namespace inProjects.Data.Queries
{
   public class TimedUserQueries
    {
        private ISqlConnectionController _controller;

        public TimedUserQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase  )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public async Task<TimedUserData> GetTimedUserByUserId(int userId)
        {  
            TimedUserData result =  await _controller.QuerySingleOrDefaultAsync<TimedUserData>( "select * from IPR.tTimedUser tu where tu.UserId = @UserId", new { UserId = userId } );
            return result;
        }

        public async Task<TimedUserData> GetTimedUserById( int timedUserId )
        {
            TimedUserData result = await _controller.QuerySingleOrDefaultAsync<TimedUserData>( "select * from IPR.tTimedUser tu where tu.TimedUserId = @TimedUserId", new { TimedUserId = timedUserId } );
            return result;
        }

        public async Task<bool> VerifyUserInSpecificGroup( int timedUserId,string typeGroup )
        {
            TimedUserData user = await GetTimedUserById( timedUserId );

            if(user == null  )
            {
                return false;
            }

            //verify group exist
            GroupQueries groupQueries = new GroupQueries( _controller );
            int idGroup = await groupQueries.GetSpecificIdGroupByZoneIdAndGroupName( user.TimePeriodId, typeGroup );

            if(idGroup == 0 )
            {
                return false;
            }

            //verify user is in the group
            int result = await _controller.QuerySingleOrDefaultAsync<int>( "SELECT ActorId FROM CK.tActorProfile where ActorId = @UserId AND GroupId = @GroupId", new { UserId = user.UserId, GroupId = idGroup } );

            if(result != 0 )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
