

using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using inProjects.Data.Data.Group;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inProjects.Data.Queries
{
    class GroupQueries
    {
        private ISqlConnectionController _controller;

        public GroupQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );

        }

        public async Task<IEnumerable<GroupData>> GetAllGroupByPeriod(int zoneId)
        {
            IEnumerable<GroupData> result = await _controller.QueryAsync<GroupData>( "SELECT * FROM CK.vGroup WHERE ZoneId = @ZoneId AND IsZone = 0 AND UserCount <> 0;", new { ZoneId = zoneId } );
            return result;
        }

        public async Task<int> GetIdSchoolByName(string schoolName)
        {
            int result = await _controller.QuerySingleOrDefault( "SELECT * FROM IPR.tSchool WHERE[Name] = @SchoolName;", new { SchoolName = schoolName } );
            return result;
        }
    }
}
