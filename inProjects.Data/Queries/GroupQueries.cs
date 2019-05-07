using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.Data.Queries
{
   public class GroupQueries
    {
        private ISqlConnectionController _controller;

        public GroupQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public GroupQueries(ISqlConnectionController controller )
        {
            _controller = controller;
        }

        public async Task<int> GetSpecificIdGroupByZoneIdAndGroupName(int zoneId,string groupName )
        {
         return await _controller.QueryFirstOrDefaultAsync<int>( "SELECT GroupId FROM CK.tGroup where GroupName = @GroupName AND ZoneId =@ZoneId;", new { ZoneId = zoneId, GroupName = groupName } );
        }

        public async Task<List<string>> GetAllGroupByZoneId(int zoneID )
        {
            IEnumerable<string> list = await _controller.QueryAsync<string>( "select GroupName from CK.vGroup where ZoneId = 0 AND IsZone = 0 OR ZoneId = @ZoneId AND IsZone = 0 group by GroupName;", new { ZoneId = zoneID } );
            return list.AsList();
        }

      
    }
}
