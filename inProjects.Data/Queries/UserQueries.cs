using CK.SqlServer;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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
    }
}
