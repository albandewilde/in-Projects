using CK.Core;
using inProjects.Data.Data.Period;
using inProjects.Data.Data.ProjectStudent;
using inProjects.Data.Queries;
using inProjects.Data.ForumsPlan;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Data.Group;

namespace inProjects.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    public class ForumController : Controller
    {
        readonly IStObjMap _stObjMap;

        public ForumController( IStObjMap stObjMap )
        {
            _stObjMap = stObjMap;
        }

        [HttpGet("GetPlan")]
        public async Task<Plan> InitPlan()
        {
            Plan plan = new Plan( 50, 30 );
            await plan.PopulateWithClassRooms();

            return plan;
        }

        [HttpGet("GetProjects")]
        public async Task<List<Project>> GetAllProjects( [FromQuery] int userId )
        {
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            List<Project> projectList = new List<Project>();

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, sqlDatabase );
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );
                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );
                IEnumerable<ProjectData> projects = await projectQueries.GetAllProjectByForum( groupData.ZoneId );

                foreach( ProjectData project in projects )
                {
                    Project p = new Project( project.ProjectStudentId, project.Name,
                        project.Semester, project.PosX, project.PosY, project.ClassRoom, project.Height, project.Width, project.ForumNumber );
                    projectList.Add( p );
                }
            }
            return projectList;
        }

        [HttpPost("SavePlan")]
        public async Task<List<int>> SavePlan([FromBody] Project[] plan)
        {
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            using( var ctx = new SqlStandardCallContext() )
            {
                ForumQueries forumQueries = new ForumQueries( ctx, sqlDatabase );
                List<int> results = await forumQueries.SavePlan( plan );
                return results;
            }
        }
    }
}
