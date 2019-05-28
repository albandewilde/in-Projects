
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
using inProjects.Data.Data.TimedUser;

namespace inProjects.WebApp.Services.CSV
{
    class CsvStudentMapping
    {
        readonly IStObjMap _stObjMap;
   
        private List<UserList> _studentLists = new List<UserList>();
        public CsvStudentMapping()
            :base()
        {

        }

        public async Task<List<UserList>> CSVReader(IFormFile path )
        {
            var streamResult = path.OpenReadStream();

            using( var reader = new StreamReader( streamResult ) )
            using( var csv = new CsvReader( reader ) )
            {
                csv.Configuration.Delimiter = ",";
                var records = csv.GetRecords<UserList>();
                var studentInfo = records.ToList();
                foreach(var e in studentInfo )
                {
                    string[] promotion = e.GroupName.Split( '-' );
                }
                return studentInfo;
            }

        }

        public async void StudentParser(List<UserList> studentList, IStObjMap stObjMap, IAuthenticationInfo authenticationInfo, string type )
        {
            using(var ctx = new SqlStandardCallContext() )
            {
                var sqlDatabase = stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
                var userTable = stObjMap.StObjs.Obtain<CustomUserTable>();
                var userTimedTable = stObjMap.StObjs.Obtain<TimedUserTable>();
                var groupTable = stObjMap.StObjs.Obtain<CustomGroupTable>();

                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );
                UserQueries userQueries = new UserQueries( ctx, sqlDatabase );

                int currentUserId = authenticationInfo.ActualUser.UserId;
                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( currentUserId );

                int zoneId = groupData.ZoneId;


                foreach( var element in studentList )
                {
                    string userName = Guid.NewGuid().ToString();
                    string firstName = element.FirstName;
                    string lastName = element.Name;
                    int timedUserType = 0;
                    string[] groupName = new string[1];
                    string mail = element.Mail;
                    int idUser = await CheckIfUserExists( stObjMap, authenticationInfo, mail, userName, firstName, lastName );

                    if( type == "student" )
                    {
                        groupName = element.GroupName.Split( '-' );
                        timedUserType = 1;
                    }
                    else if (type == "staffMember")
                    {
                        
                        groupName[0] = element.GroupName;
                        timedUserType = 2;
                    }

                    int groupId = await groupQueries.GetIdGroupByNameAndPeriod( groupName[0], zoneId );


                    TimedUserData timedUserData = await userQueries.CheckIfTimedUserExists( idUser, zoneId );

                    if (timedUserData == null )
                    {
                        await userTimedTable.CreateOrUpdateTimedUserAsync( ctx, timedUserType, zoneId, idUser );
                        groupTable.AddUser( ctx, currentUserId, groupId, idUser, true );
                        if( type == "student" && groupName[1] != "A" )
                        {
                            int sector = await groupQueries.GetIdGroupByNameAndPeriod( groupName[1], zoneId );
                            groupTable.AddUser( ctx, currentUserId, sector, idUser, true );
                        }
                    }
                    else
                    {
                        int isUserInGroup = await groupQueries.GetGroupByNamePeriodUser( groupName[0], idUser, zoneId );
                        if(isUserInGroup == 0 )
                        {
                            groupTable.AddUser( ctx, currentUserId, groupId, idUser, true );
                            if( type == "student" && groupName[1] != "A" )
                            {
                                int sector = await groupQueries.GetIdGroupByNameAndPeriod( groupName[1], zoneId );
                                groupTable.AddUser( ctx, currentUserId, sector, idUser, true );
                            }

                        }
                    }

                }
            }

        }

        public async Task<int> CheckIfUserExists( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo, string mail, string userName, string firstName, string lastName )
        {
            using( var ctx = new SqlStandardCallContext() )
            {
                var sqlDatabase = stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
                var userTable = stObjMap.StObjs.Obtain<CustomUserTable>();
                var actorEmail = stObjMap.StObjs.Obtain<ActorEMailTable>();
                var basic = stObjMap.StObjs.Obtain<IBasicAuthenticationProvider>();

                UserQueries userQueries = new UserQueries( ctx, sqlDatabase );

                int currentIdUser = authenticationInfo.ActualUser.UserId;

                int idUser = await userQueries.CheckEmail( mail );

                if(idUser != 0 )
                {
                    return idUser;
                }
                else
                {
                    string tempPwd = RandomPassword();

                    int newUserId = await userTable.CreateUserAsync( ctx, currentIdUser, userName, firstName, lastName );
                    await actorEmail.AddEMailAsync( ctx, 1, newUserId, mail, true, false );
                    await basic.CreateOrUpdatePasswordUserAsync( ctx, 1, newUserId, tempPwd );
                    return newUserId;

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
