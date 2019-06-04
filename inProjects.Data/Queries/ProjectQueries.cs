using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Data.ProjectStudent;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace inProjects.Data.Queries
{
    public class ProjectQueries
    {
        private ISqlConnectionController _controller;


        public ProjectQueries(ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public async Task<ProjectUrlData> GetUrlByProject(int projectId )
        {
            ProjectUrlData result = await _controller.QuerySingleOrDefaultAsync<ProjectUrlData>(
                "SELECT * " +
                "FROM IPR.tProjectUrl p " +
                "WHERE p.ProjectId = @ProjectId",
                new { ProjectId = projectId } );
            return result;
        }

        public async Task<UserFavProjectData> GetSPecificFavOfUser(int userId, int projectId)
        {
            UserFavProjectData result = await _controller.QuerySingleOrDefaultAsync<UserFavProjectData>(
                "SELECT * " +
                "FROM IPR.tUserFavProject uf " +
                "WHERE uf.UserId = @UserId AND uf.ProjectId = @ProjectId",
                new { ProjectId = projectId, UserId = userId } );
            return result;
        }

        public async Task<float> GetProjectGradeSpecificTimedUser( int projectId, int timedUserId )
        {
            float result = await _controller.QuerySingleOrDefaultAsync<float>(
                "select tu.Grade " +
                "from IPR.tTimedUserNoteProject tu " +
                "where tu.TimedUserId = @TimedUserId AND tu.StudentProjectId = @StudentProjectId",
                new { TimedUserId = timedUserId, StudentProjectId = projectId } );
            return result;
        }

        public async Task<IEnumerable<ProjectData>> GetAllProjectBySchool(int schoolId)
        {
            IEnumerable<ProjectData> result = await _controller.QueryAsync<ProjectData>(
                "SELECT *" +
                "FROM IPR.vProjectsDetails" +
                "WHERE ZoneId = @SchoolId",
                new { SchoolId = schoolId } );

            return result;
        }

        public async Task<List<ProjectData>> GetProjectsUser( int userId )
        {
            IEnumerable<ProjectData> result = await _controller.QueryAsync<ProjectData>(
                "select ps.ProjectStudentId, g.GroupName as [Name], ps.Logo from CK.tActorProfile ap" +
                " join IPR.tProjectStudent ps on ap.GroupId = ps.ProjectStudentId" +
                " join CK.tGroup g on g.GroupId = ps.ProjectStudentId where ap.ActorId = @UserId; ",
                new { UserId = userId } );

            return result.AsList();
        }
    }
}
