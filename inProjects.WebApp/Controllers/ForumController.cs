using CK.Core;
using inProjects.Data.Data.Period;
using inProjects.Data.Data.ProjectStudent;
using inProjects.Data.Queries;
using inProjects.WebApp.ForumsPlan;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            var projectQueries = _stObjMap.StObjs.Obtain<ProjectQueries>();
            var timedPeriodQueries = _stObjMap.StObjs.Obtain<TimedPeriodQueries>();

            PeriodData forumData = await timedPeriodQueries.GetLastPeriodBySchool( idSchool );
            IEnumerable<ProjectData> projects = await projectQueries.GetAllProjectByForum( forumData.ChildId );
            List<Project> projectList = new List<Project>();

            foreach( ProjectData project in projects )
            {
                Project p = new Project( project.Name, project.Semester, project.PosX, project.PosY );
                projectList.Add( p );
            }
            return projectList;
        }
    }
}
