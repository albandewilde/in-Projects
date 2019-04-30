using CK.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using CK.SqlServer.Setup;
using System.Threading.Tasks;
using inProjects.Data.Data.TimedUser;
using System.Collections;

namespace inProjects.Data.Queries
{
    [SqlObjectItem( "vAnswer" )]

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

        public async Task<IEnumerable<TimedStudentData>> GetAllStudentInfosByGroup(int groupId)
        {
            IEnumerable<TimedStudentData> result = await _controller.QueryAsync<TimedStudentData>( "SELECT Semester = g.GroupName, u.FirstName, u.LastName FROM CK.tGroup g JOIN CK.tActor a ON a.ActorId = g.GroupId AND g.GroupId = @GroupId LEFT OUTER JOIN CK.tActorProfile ap ON ap.GroupId = g.GroupId AND ap.ActorId <> ap.GroupId LEFT OUTER JOIN CK.tUser u ON u.UserId = ap.ActorId LEFT OUTER JOIN IPR.tTimedUser tu ON tu.UserId = u.UserId LEFT OUTER JOIN IPR.tTimedStudent ts ON ts.TimedStudentId = tu.TimedUserId;", new { GroupId = groupId } );
            return result;
        }

      }
}
