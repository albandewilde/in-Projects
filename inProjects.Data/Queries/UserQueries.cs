using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using inProjects.Data.Data.User;
using System.Threading.Tasks;
using Dapper;
using inProjects.Data.Data.User;
using inProjects.Data.Data.ProjectStudent;
using System.Collections.Generic;

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

        public async Task<UserData> GetInfosUserById(int idUser )
        {
           return await _controller.QuerySingleOrDefaultAsync<UserData>( "SELECT u.FirstName, u.LastName, am.EMail, EmailSecondary = (SELECT EMail FROM CK.tActorEMail am where am.ActorId = @UserId and am.IsPrimary = 0)" +
                " FROM CK.tUser u" +
                " join CK.tActorEMail am on u.UserId = am.ActorId" +
                " where u.UserId = @UserId and am.IsPrimary = 1; ", new {UserId = idUser } );
        }
        public async Task<List<UserFavProjectData>> GetProjectsFavUser( int idUser )
        {
            IEnumerable< UserFavProjectData> usersFav = await _controller.QueryAsync<UserFavProjectData>(" select g.GroupName, ps.Logo from IPR.tUserFavProject ufp " +
                " join IPR.tProjectStudent ps on ufp.ProjectId = ps.ProjectStudentId" +
                " join CK.tGroup g on g.GroupId = ps.ProjectStudentId" +
                " where ufp.UserId = @UserId", new { UserId = idUser } );

            return usersFav.AsList();
        }

    }
}
