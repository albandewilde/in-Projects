

using inProjects.ViewModels;
using inProjects.WebApp.Services.CSV;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    
    [AllowAnonymous]

    public  class StudentController : Controller
    {
        public StudentController()
        {

        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddStudentListCsv()
        {
            var file = Request.Form.Files[0];
            CsvStudentMapping.Test( file );
            Console.WriteLine( Request.Form.Files[0]);
            return Ok();
        }
    }
}
