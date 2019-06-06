using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using inProjects.Data.Data.User;
using System.Threading.Tasks;
using inProjects.Data.Data.TimedUser;

namespace inProjects.Data.Queries
{
    public class UserQueries
    {
        private ISqlConnectionController _controller;

        public UserQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController(sqlDefaultDatabase);        
        }

        public async Task<UserData> GetUserByName( string firstName, string lastName )
        {
            return await _controller.QueryFirstAsync<UserData>(
                "SELECT * FROM CK.tUser u WHERE u.FirstName = @FirstName AND u.LastName = @LastName",
                new { FirstName = firstName, LastName = lastName }
            );
        }

        public async Task<UserData> GetUserByEmail(string address)
        {
            return await _controller.QuerySingleAsync<UserData>(
                "select UserId, FirstName, LastName from CK.vUser where PrimaryEMail = @Address",
                new {Address = address}
            );
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
