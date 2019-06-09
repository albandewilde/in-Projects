using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Group;
using inProjects.Data.Data.ProjectStudent;
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

           return Ok(await TomlHelpers.TomlHelpers.RegisterProject(
                project.Link,
                project.ProjectType,
                projectTable,
                project.UserId,
                db,
                groupTable
            ));
        }

        [HttpGet("getAllProjects")]

        public async Task<IEnumerable<AllProjectInfoData>> GetAllProjects()
        {
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using (var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );
                UserQueries userQueries = new UserQueries( ctx, db );
                GroupQueries groupQueries = new GroupQueries( ctx, db );

                IEnumerable<AllProjectInfoData> projectData = await projectQueries.GetAllProject();
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

    }
}
