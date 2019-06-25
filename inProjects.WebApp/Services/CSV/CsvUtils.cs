
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
using inProjects.EmailJury;
using CK.DB.Zone;
using inProjects.Data.Data;
using inProjects.Data.Data.ProjectStudent;

namespace inProjects.WebApp.Services.CSV
{
    class CsvStudentMapping<T>
    {
        readonly IStObjMap _stObjMap;
        private readonly Emailer _emailSender;
        private List<UserList> _studentLists = new List<UserList>();

        public CsvStudentMapping()
            :base()
        {

        }

        public CsvStudentMapping( Emailer emailer)
        {
            _emailSender = emailer;
        }



        public async Task<List<UserList>> CSVReader(IFormFile path )
        {
            var streamResult = path.OpenReadStream();

            using( var reader = new StreamReader( streamResult, Encoding.UTF8 ) )
            using( var csv = new CsvReader( reader ) )
            {
                csv.Configuration.Delimiter = ";";
                var records = csv.GetRecords<UserList>();
                var studentInfo = records.ToList();
                foreach(var e in studentInfo )
                {
                    string[] promotion = e.GroupName.Split( '-' );
                }
                return studentInfo;
            }

        }

        public async Task<bool> UserParser(List<UserList> studentList, IStObjMap stObjMap, IAuthenticationInfo authenticationInfo, string type)
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

                    timedUserType = GetTimedUserType( type );
                    if( type == "student" ) groupName = element.GroupName.Split( '-' );                    
                    else if (type == "staffMember") groupName[0] = element.GroupName;
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
            return true;
        }

        public async Task<int> CheckIfUserExists( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo, string mail, string userName, string firstName, string lastName)
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
                    string subject = "Vous êtes invité à rejoindre la plateforme InProject";
                    string mailContent = "Afin de vous connectez a la plateforme InProject voici votre mot de passe provisoire: " + tempPwd + " il est conseillé de modifier ce mot de passe lors de votre première connection";
                    int newUserId = await userTable.CreateUserAsync( ctx, currentIdUser, userName, firstName, lastName );
                    await actorEmail.AddEMailAsync( ctx, 1, newUserId, mail, true, false );
                    await basic.CreateOrUpdatePasswordUserAsync( ctx, 1, newUserId, tempPwd );
                    await _emailSender.SendMessage( mail, subject, mailContent );
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
        public async Task<List<T>> CSVReaderProjectNumber( IFormFile path )
        {
            var streamResult = path.OpenReadStream();

            using( var reader = new StreamReader( streamResult, Encoding.UTF8) )
            using( var csv = new CsvReader( reader ) )
            {

                //csv.Configuration.Delimiter = ";";
                var records = csv.GetRecords<T>();
                var projectInfo = records.ToList();
                return projectInfo;
            }
        }

        public async Task ForumNumberParser( IStObjMap stObjMap, List<ProjectNumbers> projectNumbers, IAuthenticationInfo authenticationInfo )
        {
            using( var ctx = new SqlStandardCallContext() )
            {
                var sqlDatabase = stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
                GroupQueries projectQueries = new GroupQueries( ctx, sqlDatabase );
                ForumInfosTable forumInfosTable = stObjMap.StObjs.Obtain<ForumInfosTable>();
                int userId = authenticationInfo.ActualUser.UserId;
                GroupData groupData = await projectQueries.GetIdSchoolByConnectUser( userId );

                foreach( ProjectNumbers projectNumber in projectNumbers )
                {
                    
                    int projectId = await projectQueries.GetSpecificIdGroupByZoneIdAndGroupName( groupData.ZoneId, projectNumber.ProjectName );
                    if( projectId == 0 ) Console.WriteLine("-----" + projectNumber.ProjectName );
                    await forumInfosTable.CreateForumInfo( ctx, projectId, "", -1, -1, 4, 3, projectNumber.ProjectNumber );
                   
                }
            }

        }

        internal async Task AssignProjectToJury( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo, List<JuryInfos> juryInfos, string type )
        {
            using( var ctx = new SqlStandardCallContext() )
            {
                var sqlDatabase = stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
                var groupTable = stObjMap.StObjs.Obtain<CustomGroupTable>();
                var userTimedTable = stObjMap.StObjs.Obtain<TimedUserTable>();
                EvaluatesTable evaluatesTable = stObjMap.StObjs.Obtain<EvaluatesTable>();

                int userId = authenticationInfo.ActualUser.UserId;
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDatabase );
                GroupData groupData = await groupQueries.GetIdSchoolByConnectUser( userId );
                string userName = Guid.NewGuid().ToString();
                UserQueries userQueries = new UserQueries( ctx, sqlDatabase );
                TimedUserQueries timedUserQueries = new TimedUserQueries( ctx, sqlDatabase );
                ProjectQueries projectQueries = new ProjectQueries( ctx, sqlDatabase );
                TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries( ctx, sqlDatabase );
                ForumQueries forumQueries = new ForumQueries( ctx, sqlDatabase );
                //get the Begdate of the periode
                DateTime begDate = await timedPeriodQueries.GetBegDateOfPeriod( groupData.ZoneId );

                foreach( JuryInfos juryInfo in juryInfos )
                {
                    int enterpriseId = 0;
                    string groupName = "Jury " + juryInfo.Jury + "-" + begDate.Year+ "/" + begDate.Month;
                    int timedUserType = 0;
                    //Check if the jury group exists or not at a moment
                    int groupId = await groupQueries.GetSpecificIdGroupByZoneIdAndGroupName( groupData.ZoneId, groupName );
                    if( groupId == 0 )
                    {
                        groupId = await groupTable.CreateGroupAsync( ctx, userId );
                        await groupTable.Naming.GroupRenameAsync( ctx, userId, groupId, groupName );
                    }

                    //Check if the enterprise group exists or not at a moment
                    enterpriseId = await groupQueries.GetSpecificIdGroupByZoneIdAndGroupName( groupData.ZoneId, juryInfo.Entreprise );
                    if( enterpriseId == 0 )
                    {
                        enterpriseId = await groupTable.CreateGroupAsync( ctx, userId );
                        await groupTable.Naming.GroupRenameAsync( ctx, userId, enterpriseId, juryInfo.Entreprise );
                    }

                    //Check if the user exists
                    int idJury = await CheckIfUserExists( stObjMap, authenticationInfo, juryInfo.Mail, userName, juryInfo.Prenom, juryInfo.Nom );

                    //get the timed user type
                    timedUserType = GetTimedUserType( type );

                    //check if the timed user exists
                    TimedUserData timedUserData = await userQueries.CheckIfTimedUserExists( idJury, groupData.ZoneId );
                    if(timedUserData == null )
                    {
                        await userTimedTable.CreateOrUpdateTimedUserAsync( ctx, timedUserType, groupData.ZoneId, idJury );

                    }
                    else
                    {
                        bool isJury = await timedUserQueries.IsJury( timedUserData.TimedUserId );
                        if(!isJury) await userTimedTable.CreateOrUpdateTimedUserAsync( ctx, timedUserType, groupData.ZoneId, idJury );
                    }

                    //get the project id by forumnumber
                    ProjectData projectId;
                    string groupName1 = "Vous êtes libre";
                    string groupName2 = "Vous êtes libre";
                    string groupName3 = "Vous êtes libre";
                    string groupName4 = "Vous êtes libre";
                    TimeSpan timeSpan1 = new TimeSpan( 13, 30, 0 );
                    TimeSpan timeSpan2 = new TimeSpan( 14, 15, 0 );
                    TimeSpan timeSpan3 = new TimeSpan( 15, 00, 0 );
                    TimeSpan timeSpan4 = new TimeSpan( 15, 45, 0 );

                    if( juryInfo.Groupe1 != 0 )
                    {
                        projectId = await projectQueries.GetProjectIdByForumNumberAndPeriod( juryInfo.Groupe1, groupData.ZoneId );
                        begDate = begDate.Date + timeSpan1;
                        await evaluatesTable.EvaluateOrUpdateGradeProject( ctx, groupId, projectId.ProjectStudentId, -1, begDate );
                        groupName1 = projectId.GroupName;
                    }

                    if(juryInfo.Groupe2 != 0 )
                    {
                        projectId = await projectQueries.GetProjectIdByForumNumberAndPeriod( juryInfo.Groupe2, groupData.ZoneId );
                        begDate = begDate.Date + timeSpan2;
                        await evaluatesTable.EvaluateOrUpdateGradeProject( ctx, groupId, projectId.ProjectStudentId, -1, begDate );
                        groupName2 = projectId.GroupName;

                    }

                    if(juryInfo.Groupe3 != 0 )
                    {
                        projectId = await projectQueries.GetProjectIdByForumNumberAndPeriod( juryInfo.Groupe3, groupData.ZoneId );
                        begDate = begDate.Date + timeSpan3;
                        await evaluatesTable.EvaluateOrUpdateGradeProject( ctx, groupId, projectId.ProjectStudentId, -1, begDate );
                        groupName3 = projectId.GroupName;

                    }
                    if(juryInfo.Groupe4 != 0 )
                    {
                        projectId = await projectQueries.GetProjectIdByForumNumberAndPeriod( juryInfo.Groupe4, groupData.ZoneId );
                        begDate = begDate.Date + timeSpan4;
                        await evaluatesTable.EvaluateOrUpdateGradeProject( ctx, groupId, projectId.ProjectStudentId, -1, begDate );
                        groupName4 = projectId.GroupName;

                    }

                    IEnumerable<ForumData> infoMail = await forumQueries.ForumInfoByJury( groupName );
                    int nbProject = infoMail.Count();

                    string subject = "Votre participation au Forum PI D'IN'TECH";
                    string mailContent = "Bonjour " + juryInfo.Prenom + " " + juryInfo.Nom + ". Vous participez au forum informatique de l'école IN'TECH le " + begDate.Day + " " + begDate.Month + " " + begDate.Year + ". Vous appartenez au jury " + groupName + ". Vous aurez l'occasion de juger les projets " + groupName1 + " à " + timeSpan1 + ", " + groupName2 + " à " + timeSpan2 + ", " + groupName3 + " à " + timeSpan3 + ", " + groupName4 + "à " + timeSpan4;
                    await _emailSender.SendMessage( juryInfo.Mail, subject, mailContent );



                }
            }
        }

        public int GetTimedUserType(string type )
        {
            if( type == "student" ) return 1;
            if( type == "staffMember" ) return 2;
            if( type == "jury" ) return 3;
            return 0;

        }

    }

}
