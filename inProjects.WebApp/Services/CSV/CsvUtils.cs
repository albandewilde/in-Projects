
using Microsoft.AspNetCore.Http;
using System;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using CK.Core;
using CK.Auth;
using System.Threading.Tasks;
using inProjects.Data;
using CK.SqlServer;
using inProjects.Data.Queries;
using CK.SqlServer.Setup;
using inProjects.Data.Data.Group;
using CK.DB.Actor;

namespace inProjects.WebApp.Services.CSV
{
    class CsvStudentMapping
    {
        readonly IStObjMap _stObjMap;
   
        private List<StudentList> _studentLists = new List<StudentList>();
        public CsvStudentMapping()
            :base()
        {

        }

        public async Task<List<StudentList>> CSVReader(IFormFile path )
        {
            var streamResult = path.OpenReadStream();

            using( var reader = new StreamReader( streamResult ) )
            using( var csv = new CsvReader( reader ) )
            {
                csv.Configuration.Delimiter = ",";
                var records = csv.GetRecords<StudentList>();
                var studentInfo = records.ToList();
                foreach(var e in studentInfo )
                {
                    string[] promotion = e.Promotion.Split( '-' );
                }
                return studentInfo;
            }

        }

        public async void StudentParser(List<StudentList> studentList, IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            using(var ctx = new SqlStandardCallContext() )
            {
                var sqlDatabase = stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
                var userTable = stObjMap.StObjs.Obtain<CustomUserTable>();
                var userTimedTable = stObjMap.StObjs.Obtain<TimedUserTable>();
                var groupTable = stObjMap.StObjs.Obtain<CustomGroupTable>();

                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );

                int userId = authenticationInfo.ActualUser.UserId;
                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );
                int zoneId = groupData.ZoneId;

                foreach( var element in studentList )
                {
                    string userName = Guid.NewGuid().ToString();
                    string firstName = element.FirstName;
                    string lastName = element.Name;
                    string[] promotions = element.Promotion.Split('-');

                    int newUserId = await userTable.CreateUserAsync( ctx, userId, userName, firstName, lastName );
                    await userTimedTable.CreateOrUpdateTimedUserAsync( ctx, 1, zoneId, newUserId );

                    int promotion = await groupQueries.GetIdGroupByNameAndPeriod( promotions[0], zoneId );
                    groupTable.AddUser( ctx, userId, promotion, newUserId, true );

                    if(promotions[1] != "A" )
                    {
                        int sector = await groupQueries.GetIdGroupByNameAndPeriod( promotions[1], zoneId );
                        groupTable.AddUser( ctx, userId, sector, newUserId, true );
                    }

                   
                }
            }

        }


    }



   
}
