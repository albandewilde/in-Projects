using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.Data.Queries
{

    public class AclQueries
    {
        private ISqlConnectionController _controller;

        public AclQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public async Task<bool> VerifySuperiorOrEqualGrantLevelUserByUserId(int grantLevel, int aclId, int userId )
        {
            int result = await _controller.QuerySingleOrDefaultAsync<int>
                ( "select * from CK.vAclActor a where a.ActorId = @ActorId and a.AclId = @Acl and a.GrantLevel >= @GrantLevel",
                         new { ActorId = userId, Acl = aclId, GrantLevel = grantLevel } );
            if(result == 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
