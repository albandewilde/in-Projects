using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Group;
using inProjects.Data.Data.Period;
using inProjects.Data.Data.ProjectStudent;
using inProjects.Data.Data.TimedUser;
using inProjects.Data.Data.User;
using inProjects.Data.Queries;
using inProjects.Data.Res.Model;
using inProjects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController: Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public ProjectController(IStObjMap stObjMap, IAuthenticationInfo authenticationInfo)
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }


        [HttpPost("submitProject")]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitProject([FromBody] SubmitProjectModel project)
        {
            ProjectStudentTable projectTable = _stObjMap.StObjs.Obtain<ProjectStudentTable>();           
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            CustomGroupTable groupTable = _stObjMap.StObjs.Obtain<CustomGroupTable>();
            ProjectUrlTable projectUrl = _stObjMap.StObjs.Obtain<ProjectUrlTable>();

           return Ok(await TomlHelpers.TomlHelpers.RegisterProject(
                project.Link,
                project.ProjectType,
                projectTable,
                project.UserId,
                db,
                groupTable,
                _stObjMap,
                projectUrl
            ));
        }

        [HttpGet( "getInfosProject" )]
        [AllowAnonymous]
        public async Task<IActionResult> GetInfosProject( int idProject, int idZone )
        {
            ProjectStudentTable projectTable = _stObjMap.StObjs.Obtain<ProjectStudentTable>();
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );

                ProjectDetailsData projectDetails = new ProjectDetailsData
                {
                    Project = await projectQueries.GetDetailProject( idProject ),

                    Students = await projectQueries.GetAllUsersOfProject(idProject)
                };

                List<string> listGroups = await projectQueries.GetGroupsOfProject( idProject);

                listGroups = listGroups.FindAll( x => x.StartsWith( "S0" ) || x == "IL" || x == "SR" );
                string groups = listGroups[0];

                if( listGroups.Count >= 2 )
                {
                    for( int i = 1; i < listGroups.Count; i++ )
                    {
                        groups += "-" + listGroups[i];
                    }
                }
               

                projectDetails.Project.Semester = groups;

                return Ok( projectDetails );

            }

        }

        [HttpGet( "verifyProjectFav" )]
        public async Task<IActionResult> VerifyProjectFav( int idProject )
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();


            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, sqlDataBase );

                bool exist = await projectQueries.IsProjectFav( idProject, userId );

                return Ok( exist );
            }
        }

        [HttpGet( "favProject" )]
        [AllowAnonymous]
        public async Task<IActionResult> FavProject( int idProject)
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            UserFavProjectTable favTable = _stObjMap.StObjs.Obtain<UserFavProjectTable>();
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
               await favTable.FavOrUnfavProject( ctx, userId, idProject );

               return Ok( );

            }

        }

        [HttpGet("getAllProjects")]
        public async Task<IEnumerable<AllProjectInfoData>> GetAllProjects()
        {
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            int userId = _authenticationInfo.ActualUser.UserId;

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );
                UserQueries userQueries = new UserQueries( ctx, db );
                GroupQueries groupQueries = new GroupQueries( ctx, db );

                IEnumerable<AllProjectInfoData> projectData = await projectQueries.GetAllProject(userId);
                for( var i = 0; i < projectData.Count(); i++ )
                {
                    IEnumerable<UserByProjectData> userByProject = await userQueries.GetUserByProject( projectData.ElementAt( i ).ProjectStudentId );
                    projectData.ElementAt( i ).BegDate = userByProject.ElementAt( 0 ).BegDate;
                    projectData.ElementAt( i ).EndDate = userByProject.ElementAt( 0 ).EndDate;

                    foreach( var e in userByProject )
                    {
                        IEnumerable<GroupData> groupDatas = await groupQueries.GetAllGroupByTimedUser( e.TimedUserId );
                        projectData.ElementAt( i ).FirstName.Add( e.FirstName );
                        projectData.ElementAt( i ).LastName.Add( e.LastName );
                        projectData.ElementAt( i ).TimedUserId.Add( e.TimedUserId );
                        projectData.ElementAt( i ).UserId.Add( e.UserId );
                    }
                }
                return projectData;
            }
        }

        [HttpGet( "getAllTypeProjectsOfASchool" )]
        public async Task<IActionResult> GetAllTypeProjectsOfASchool(int idSchool, char type)
        {
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            int userId = _authenticationInfo.ActualUser.UserId;

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );
                TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries( ctx, db );
                TimedUserQueries timedUserQueries = new TimedUserQueries( ctx, db );
                UserQueries userQueries = new UserQueries( ctx, db );
                GroupQueries groupQueries = new GroupQueries( ctx, db );

                PeriodData periodData = await timedPeriodQueries.GetLastPeriodBySchool( idSchool );
                //Implementer check date period

                TimedUserData timedUserData = await timedUserQueries.GetTimedUser( userId, periodData.ChildId );

                if( timedUserData == null )
                {
                    timedUserData = new TimedUserData
                    {
                        TimedUserId = 0
                    };
                }

                IEnumerable<AllProjectInfoData> projectData = await projectQueries.GetAllTypeProjectSpecificToSchool( periodData.ChildId,type,timedUserData.TimedUserId);
                for( var i = 0; i < projectData.Count(); i++ )
                {
                    IEnumerable<UserByProjectData> userByProject = await userQueries.GetUserByProject( projectData.ElementAt( i ).ProjectStudentId );
                    projectData.ElementAt( i ).BegDate = userByProject.ElementAt( 0 ).BegDate;
                    projectData.ElementAt( i ).EndDate = userByProject.ElementAt( 0 ).EndDate;

                    foreach( var e in userByProject )
                    {
                        IEnumerable<GroupData> groupDatas = await groupQueries.GetAllGroupByTimedUser( e.TimedUserId );
                        projectData.ElementAt( i ).FirstName.Add( e.FirstName );
                        projectData.ElementAt( i ).LastName.Add( e.LastName );
                        projectData.ElementAt( i ).TimedUserId.Add( e.TimedUserId );
                        projectData.ElementAt( i ).UserId.Add( e.UserId );
                    }
                }
                return Ok(projectData);
            }
        }
        [HttpPost( "noteProject" )]
        [AllowAnonymous]
        public async Task<IActionResult> NoteProject([FromBody] NoteProjectViewModel model )
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            TimedUserNoteProjectTable timedUserNoteProjectTable = _stObjMap.StObjs.Obtain<TimedUserNoteProjectTable>();
            TimedUserTable timedUserTable = _stObjMap.StObjs.Obtain<TimedUserTable>();
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );
                TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries( ctx, db );
                TimedUserQueries timedUserQueries = new TimedUserQueries( ctx, db );
                GroupQueries groupQueries = new GroupQueries( ctx, db );

                PeriodData periodData = await timedPeriodQueries.GetLastPeriodBySchool( model.SchoolId );

                TimedUserData timedUserData = await timedUserQueries.GetTimedUser( userId, periodData.ChildId );

                if(timedUserData == null )
                {
                    TimedUserStruct timedUser = await timedUserTable.CreateOrUpdateTimedUserAsyncWithType( ctx, TypeTimedUser.Anon, periodData.ChildId, userId );
                    timedUserData = new TimedUserData
                    {
                        TimedUserId = timedUser.TimedUserId
                    };
                }
                                           
                await timedUserNoteProjectTable.AddOrUpdateNote( ctx, timedUserData.TimedUserId, model.ProjectId, model.Grade );
                return Ok();

            }

        }

    }
}
