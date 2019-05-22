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
    }
}
