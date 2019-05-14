using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using inProjects.Data.Data.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.Data.Queries
{
    public class UserQueries
    {
        private ISqlConnectionController _controller;

        public UserQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );        
        }

        public async Task<UserData> GetUserByName( string firstName, string lastName )
        {
            return await _controller.QueryFirstAsync(
                "SELECT * FROM CK.tUser WHERE firstName = @firstName AND lastName = lastName",
                new { firstName = firstName, lastName = lastName }
            );
        }
    }
}
