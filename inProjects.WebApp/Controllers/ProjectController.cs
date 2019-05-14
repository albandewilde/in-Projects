using System.Threading.Tasks;
using CK.Core;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController: Controller
    {
        readonly IStObjMap _stObjMap;
        public ProjectController(IStObjMap stObjMap)
        {
            _stObjMap = stObjMap;
        }


        [HttpPost("submitProject")]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitProject([FromBody] SubmitProjectModel project)
        {
            ProjectStudentTable projectTable = _stObjMap.StObjs.Obtain<ProjectStudentTable>();
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            return Ok(TomlHelpers.TomlHelpers.RegisterProject(project.Link, project.ProjectType, projectTable, project.UserId, db));
        }
    }
}