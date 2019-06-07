using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.ProjectStudent;
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

        public ProjectController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
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

           return Ok(await TomlHelpers.TomlHelpers.RegisterProject(
                project.Link,
                project.ProjectType,
                projectTable,
                project.UserId,
                db,
                groupTable
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

                List<string> listGroups = await projectQueries.GetGroupsOfProjectWithTimedUser( idProject, idZone );
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
    }
}
