
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
using CK.DB.Actor.ActorEMail;
using System.Text;
using CK.DB.Auth;

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
                var actorEmail = stObjMap.StObjs.Obtain<ActorEMailTable>();
                var basic = stObjMap.StObjs.Obtain<IBasicAuthenticationProvider>();

                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );
                UserQueries userQueries = new UserQueries( ctx, sqlDatabase );

                int userId = authenticationInfo.ActualUser.UserId;
                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );
                int zoneId = groupData.ZoneId;


                foreach( var element in studentList )
                {
                    string userName = Guid.NewGuid().ToString();
                    string firstName = element.FirstName;
                    string lastName = element.Name;
                    string[] promotions = element.Promotion.Split('-');
                    string mail = element.Mail;
                    string tempPwd = RandomPassword();

                    int exists = await userQueries.CheckEmail( mail );

                    int newUserId = await userTable.CreateUserAsync( ctx, userId, userName, firstName, lastName );
                    await userTimedTable.CreateOrUpdateTimedUserAsync( ctx, 1, zoneId, newUserId );

                    int promotion = await groupQueries.GetIdGroupByNameAndPeriod( promotions[0], zoneId );
                    groupTable.AddUser( ctx, userId, promotion, newUserId, true );

                    if(promotions[1] != "A" )
                    {
                        int sector = await groupQueries.GetIdGroupByNameAndPeriod( promotions[1], zoneId );
                        groupTable.AddUser( ctx, userId, sector, newUserId, true );
                    }

                    if( exists == 0 )
                    {
                        await actorEmail.AddEMailAsync( ctx, 1, newUserId, mail, true, false );
                        await basic.CreateOrUpdatePasswordUserAsync( ctx, 1, newUserId, tempPwd );
                    }


                }
            }

        }

        public string RandomPassword(int size = 8)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for(int i = 0; i < size; i++ )
            {
                ch = Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * random.NextDouble() + 65 ) ) );
                builder.Append( ch );
            }

            return builder.ToString();

        }


    }



   
}
