using CK.Core;
using Microsoft.AspNetCore.Mvc;
using inProjects.ViewModels;
using inProjects.Data.Queries;
using CK.Auth;
using inProjects.Data.Data.Group;
using inProjects.Data.Data.Period;
using System.Collections.Generic;
using inProjects.Data.Data.TimedUser;
using System.Linq;
using System.Threading.Tasks;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class UserController
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public UserController(IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }

        [HttpGet( "getStudentList" )]

        public async Task<List<TimedStudentData>> GetAllStudentByPeriod()
        {
            var sqlDatabase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            IEnumerable<TimedStudentData> studentList = new List<TimedStudentData>();
            List<TimedStudentData> timedStudentDatas = new List<TimedStudentData>();


            using( var ctx = new SqlStandardCallContext() )
            {
                TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries( ctx, sqlDatabase );
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );
                TimedUserQueries timedUserQueries = new TimedUserQueries( ctx, sqlDatabase );
                int userId = _authenticationInfo.ActualUser.UserId;


                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );
    //PeriodData periodData = await timedPeriodQueries.GetLastPeriodBySchool( groupData.ZoneId );
                IEnumerable<GroupData> groupList = await groupQueries.GetAllGroupByPeriod( groupData.ZoneId );

                List<GroupData> groupFinal = groupList.ToList();
                for( int i = 0; i < groupFinal.Count; i++ )
                {
                    studentList = await timedUserQueries.GetAllStudentInfosByGroup( groupFinal[i].GroupId );
                    timedStudentDatas.AddRange( studentList );
                }

            }

            return timedStudentDatas;

        }


    }
}
