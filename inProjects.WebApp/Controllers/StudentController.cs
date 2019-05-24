

using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Group;
using inProjects.Data.Queries;
using inProjects.WebApp.Services;
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

            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            PeriodServices periodServices = new PeriodServices();

            using( var ctx = new SqlStandardCallContext())
            {
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );
                AclQueries aclQueries = new AclQueries( ctx, sqlDatabase );
                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );

                // User must have the rights to do this action
                if(await aclQueries.VerifyGrantLevelByUserId(112, await aclQueries.GetAclIdBySchoolId(groupData.ParentZoneId), userId, Operator.SuperiorOrEqual) == false )
                {
                    Result result = new Result( Status.Unauthorized, "Vous n'etes pas autorisé à utiliser cette fonctionnalité !" );
                    return this.CreateResult( result );
                }

                List<StudentList> studentResult = await csvStudentMapping.CSVReader( file );
                bool isInPeriod = await periodServices.CheckInPeriod( _stObjMap, _authenticationInfo );

                // The school in wich the user is need to be in a period when the action is done
                if( !isInPeriod )
                {
                    Result result = new Result( Status.Unauthorized, "A la date d'aujourd'hui votre etablissement n'est dans une aucune periode" );
                    return this.CreateResult( result );
                }
                csvStudentMapping.StudentParser( studentResult, _stObjMap, _authenticationInfo );              
            }

            return Ok();
        }

       

        
    }
}
