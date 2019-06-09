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
            Plan plan = new Plan( 28, 24 );
            await plan.PopulateWithClassRooms();

            return plan;
        }

        [HttpGet]
        public async Task<List<Project>> GetAllProjects( int idSchool )
        {
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            List<Project> projectList = new List<Project>();

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, sqlDatabase );
                TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries( ctx, sqlDatabase );
                PeriodData forumData = await timedPeriodQueries.GetLastPeriodBySchool( idSchool );
                IEnumerable<ProjectData> projects = await projectQueries.GetAllProjectByForum( forumData.ZoneId );

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
        public async Task SavePlan([FromBody] Project[] plan)
        {
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            using( var ctx = new SqlStandardCallContext() )
            {
                ForumQueries forumQueries = new ForumQueries( ctx, sqlDatabase );
                int result = await forumQueries.SavePlan( plan );
            }
        }
    }
}
