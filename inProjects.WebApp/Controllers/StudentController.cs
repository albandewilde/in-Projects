

using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Queries;
using inProjects.WebApp.Services.CSV;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    
    [AllowAnonymous]

    public  class StudentController : Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;
        public StudentController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            _authenticationInfo = authenticationInfo;
            _stObjMap = stObjMap;
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddStudentListCsv()
        {
            CsvStudentMapping csvStudentMapping = new CsvStudentMapping();
            var file = Request.Form.Files[0];

            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext())
            {
                AclQueries aclQueries = new AclQueries( ctx, sqlDatabase );

                List<StudentList> studentResult = await csvStudentMapping.CSVReader( file );

            }

            return Ok();
        }

       

        
    }
}
