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

        public async Task<TimedUserData> GetTimedUser(int userId)
        {
            
            TimedUserData result =  await _controller.QuerySingleOrDefaultAsync<TimedUserData>( "select * from IPR.tTimedUser tu where tu.UserId = @UserId", new { UserId = userId } );
            return result;
        }

    }
}
