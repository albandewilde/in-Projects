using System;
using System.Threading.Tasks;
using CK.AspNet.Auth;
using inProjects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {

        public UserController()
        {
        }


        [HttpPost("submitProject")]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitProject([FromBody] SubmitProjectModel project)
        {
            return Ok(TomlHelpers.TomlHelpers.RegisterProject(project.Link, project.ProjectType));
        }
    }
}