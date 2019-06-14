using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Data.ProjectStudent;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using inProjects.Data.Data.User;

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

        public async Task<ProjectUserFavData> GetSPecificFavOfUser(int userId, int projectId)
        {
            ProjectUserFavData result = await _controller.QuerySingleOrDefaultAsync<ProjectUserFavData>(
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

        public async Task<ProjectData> GetDetailProject( int idProject )
        {

            ProjectData project = await _controller.QueryFirstOrDefaultAsync<ProjectData>(
                        "  SELECT ps.Logo, ps.Slogan, ps.Pitch, g.GroupName as [Name], pu.[Url]" +
                        " from IPR.tProjectStudent ps" +
                        " join CK.tGroup g on g.GroupId = ps.ProjectStudentId" +
                        " left outer join IPR.tProjectUrl pu on pu.ProjectId = ps.ProjectStudentId" +
                        " where ProjectStudentId =  @ProjectId",
                        new { ProjectId = idProject } );

            string technos = await _controller.QueryFirstOrDefaultAsync<string>(
                         "  SELECT ckt.TraitName as Technologies" +
                         " from IPR.tProjectStudent ps" +
                         " join CK.tGroup g on g.GroupId = ps.ProjectStudentId" +
                         " join CK.tCKTrait ckt on ps.TraitId = ckt.CKTraitId" +
                         " where ProjectStudentId =  @ProjectId",
                         new { ProjectId = idProject } );

            project.Technologies = technos.Split( ';' ).AsList();

            return project;
        }

        public async Task<List<UserData>> GetAllUsersOfProject( int idProject )
        {
             IEnumerable<UserData> result = await _controller.QueryAsync<UserData>(
                         "SELECT u.FirstName, u.LastName" +
                         " from CK.tGroup g join CK.tActorProfile ap on g.GroupId = ap.GroupId" +
                         " join CK.tUser u on u.UserId = ap.ActorId" +
                         " where g.GroupId = @ProjectId and ap.ActorId <> @ProjectId ",
                         new { ProjectId = idProject } );

            return result.AsList();
        }

        public async Task<List<string>> GetGroupsOfProject ( int idProject)
        {
            IEnumerable<string> result = await _controller.QueryAsync<string>( "SELECT g1.GroupName FROM IPR.tProjectStudent ps JOIN CK.vGroup g ON g.GroupId = ps.ProjectStudentId JOIN CK.tActorProfile ap ON ap.GroupId = g.GroupId JOIN CK.tUser u ON u.UserId = ap.ActorId LEFT OUTER JOIN CK.tActorProfile ap1 ON u.UserId = ap1.ActorId LEFT OUTER JOIN CK.vGroup g1 ON ap1.GroupId =  g1.GroupId WHERE g.IsZone = 0 AND g1.IsZone = 0 AND g.GroupName <> g1.GroupName AND g.GroupId = @ProjectId and ap1.ActorId <>0 GROUP BY g1.GroupName", new { ProjectId = idProject} );

            return result.AsList();
        }

        public async Task<bool> IsProjectFav(int idProject, int userId )
        {
            int project = await _controller.QuerySingleOrDefaultAsync<int>
                ( "SELECT * FROM IPR.tUserFavProject where UserId = @UserId and ProjectId = @ProjectId"
                , new { ProjectId = idProject, UserId = userId } );

            if(project == 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public async Task<IEnumerable<AllProjectInfoData>> GetAllProject(int userId)
        {
            IEnumerable<AllProjectInfoData> result = await _controller.QueryAsync<AllProjectInfoData>( "SELECT ps.ProjectStudentId, ps.Logo, ps.Slogan, ps.Pitch, ps.LeaderId, ps.Type, t.TraitName, g.GroupName, g.ZoneId, isFav = uf.UserId FROM  IPR.tProjectStudent ps LEFT OUTER JOIN CK.tCKTrait t ON t.CKTraitId = ps.TraitId LEFT OUTER JOIN CK.tGroup g ON g.GroupId = ps.ProjectStudentId LEFT OUTER JOIN IPR.tUserFavProject uf ON uf.ProjectId = ps.ProjectStudentId WHERE uf.UserId = @UserId OR uf.UserId is null ;", new { UserId = userId } );
            return result;
        }

        public async Task<IEnumerable<ProjectData>> GetAllProjectByForum( int forumId )
        {
            IEnumerable<ProjectData> result = await _controller.QueryAsync<ProjectData>(
                "SELECT *" +
                "FROM IPR.vForumProjectInfos" +
                "WHERE ForumId = @ForumId",
                new { ForumId = forumId } );

            return result;
        }
    }
}
