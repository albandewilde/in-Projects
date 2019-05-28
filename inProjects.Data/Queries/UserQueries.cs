using CK.SqlServer;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using inProjects.Data.Data.TimedUser;

namespace inProjects.Data.Queries
{
    public class UserQueries
    {
        private ISqlConnectionController _controller;

        public UserQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public async Task<int> VerifyGroupUser(int idUser,int idGroup )
        {
            int isExist =  await _controller.QuerySingleOrDefaultAsync<int>( "select ActorId from CK.tActorProfile where GroupId = @GroupId and ActorId = @ActorId;", new { GroupId = idGroup, ActorId = idUser } );
            return isExist;
        }

        public async Task<int> CheckEmail(string mail )
        {
            int exists =  await _controller.QuerySingleOrDefaultAsync<int>( " SELECT ActorId FROM CK.tActorEMail WHERE EMail = @Mail", new { Mail = mail } );
            return exists;
        }

        public async Task<TimedUserData> CheckIfTimedUserExists(int idUser, int idPeriod )
        {
            TimedUserData exists = await _controller.QuerySingleOrDefaultAsync<TimedUserData>( "SELECT * FROM IPR.tTimedUser tu WHERE tu.UserId = @UserId AND tu.TimePeriodId = @PeriodId", new { UserId = idUser, PeriodId = idPeriod } );
            return exists;
        }
    }
}
