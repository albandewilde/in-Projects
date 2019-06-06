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
            //return await _controller.QueryFirstOrDefaultAsync<ProjectData>(
            //             "  SELECT ps.Logo, ps.Slogan, ps.Pitch, g.GroupName as [Name], ckt.TraitName as Technologies" +
            //             " from IPR.tProjectStudent ps" +
            //             " join CK.tGroup g on g.GroupId = ps.ProjectStudentId" +
            //             " join CK.tCKTrait ckt on ps.TraitId = ckt.CKTraitId" +
            //             " where ProjectStudentId =  @ProjectId",
            //             new { ProjectId = idProject } );

            ProjectData project = await _controller.QueryFirstOrDefaultAsync<ProjectData>(
                        "  SELECT ps.Logo, ps.Slogan, ps.Pitch, g.GroupName as [Name]" +
                        " from IPR.tProjectStudent ps" +
                        " join CK.tGroup g on g.GroupId = ps.ProjectStudentId" +
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

        public async Task<List<string>> GetGroupsOfProjectWithTimedUser ( int idProject, int idZone )
        {
            IEnumerable<string> result = await _controller.QueryAsync<string>(
                        "SELECT g.GroupName FROM CK.tGroup g" +
                        " join CK.tActorProfile ap on g.GroupId = ap.GroupId" +
                        " join IPR.tTimedUser tu on tu.UserId = ap.ActorId" +
                        " where tu.TimePeriodId = (SELECT GroupId FROM CK.tActorProfile where ActorId = @ProjectId and GroupId <> @IdZone and GroupId <> @ProjectId )" +
                        " and tu.TimedUserId = (SELECT LeaderId FROM IPR.tProjectStudent where ProjectStudentId = @ProjectId)" +
                        " and g.GroupName like 'S%' or g.GroupName like 'IL' or g.GroupName like 'SR'" +
                        " group by g.GroupName" +
                        " ORDER BY g.GroupName DESC",
                        new { ProjectId = idProject, IdZone = idZone } );

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

    }
}
