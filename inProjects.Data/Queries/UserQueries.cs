using CK.SqlServer;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using inProjects.Data.Data.User;

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

        public async Task<UserData> GetInfosUserById(int idUser )
        {
           return await _controller.QuerySingleOrDefaultAsync<UserData>( "SELECT u.FirstName, u.LastName, am.EMail, EmailSecondary = (SELECT EMail FROM CK.tActorEMail am where am.ActorId = @UserId and am.IsPrimary = 0)" +
                " FROM CK.tUser u" +
                " join CK.tActorEMail am on u.UserId = am.ActorId" +
                " where u.UserId = @UserId and am.IsPrimary = 1; ", new {UserId = idUser } );
        }
    }
}
