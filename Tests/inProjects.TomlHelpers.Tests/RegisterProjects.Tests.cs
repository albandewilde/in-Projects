using CK.Core;
using CK.DB.Actor.ActorEMail;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Period;
using inProjects.Data.Data.TimedUser;
using inProjects.Data.Queries;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using static CK.Testing.DBSetupTestHelper;

namespace inProjects.TomlHelpers.Tests
{
    [TestFixture]
    public class RegisterProjects
    {
        [Test]
        public async Task a__make_somes_inserts_or_tests()
        {
            ProjectStudentTable projectStudent = TestHelper.StObjMap.StObjs.Obtain<ProjectStudentTable>();
            SqlDefaultDatabase db = TestHelper.StObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            CustomGroupTable groupTable = TestHelper.StObjMap.StObjs.Obtain<CustomGroupTable>();
            TimedUserTable userTimed = TestHelper.StObjMap.StObjs.Obtain<TimedUserTable>();
            ActorEMailTable actorEmail = TestHelper.StObjMap.StObjs.Obtain<ActorEMailTable>();

            // declare students
            string[] members = new string[]{"Julie Agopian", "Arthur Cheng", "Dan Chiche", "Melvin Delpierre", "Alban DeWilde"};
            string[] membersEmail = new string[]{"agopian@intechinfo.fr", "acheng@intechinfo.fr", "chiche@intechinfo.fr", "delpierre@intechinfo.fr", "dewilde@intechinfo.fr"};
            int[] membersId = new int[5];


            var userTable = TestHelper.StObjMap.StObjs.Obtain<CustomUserTable>();
            var school = TestHelper.StObjMap.StObjs.Obtain<SchoolTable>();
            var timePeriod = TestHelper.StObjMap.StObjs.Obtain<TimePeriodTable>();
            DateTime dateBegin = DateTime.Now;
            DateTime dateEnd = DateTime.Now.AddDays(1);

            using(SqlStandardCallContext ctx = new SqlStandardCallContext())
            {
                // get school id
                GroupQueries groupeQueries = new GroupQueries(ctx, db);
                int schoolId = await groupeQueries.GetIdSchoolByName("IN'TECH");

                // create the time period
                int periodId = await timePeriod.CreateTimePeriodAsync(ctx, 1, dateBegin, dateEnd, "S", schoolId);

                // create group S5
                int idGroup = await groupTable.CreateGroupAsync( ctx, 1, periodId );
                await groupTable.Naming.GroupRenameAsync( ctx, 1, idGroup, "S5" );

                // inserts students and join email
                int idx = 0;
                foreach (string member in members)
                {
                    string[] name = member.Split();
                    int userId = await userTable.CreateUserAsync(ctx, 1, Guid.NewGuid().ToString(), name[0], name[1]);
                    membersId[idx] = userId;
                    TimedUserStruct timedUser = await userTimed.CreateOrUpdateTimedUserAsync(ctx, 1, periodId, userId);
                    await groupTable.AddUserAsync(ctx, 1, idGroup, userId);
                    await actorEmail.AddEMailAsync(ctx, userId, userId, membersEmail[idx], true);

                    idx += 1;
                }
            }
        }

        //[TestCase("this isn't a url", 0, false)]
        //[TestCase("http://example.com/", 0, false)]
        //[TestCase("https://drive.google.com/open?id=1Jce4M06bOOGLLrufoaMnfhOH30GopdXC", 0, false)]    // wrong file (faled to parse)
        //[TestCase("https://drive.google.com/open?id=1TQ5QMJ0QdB4VRPg3WdcxSBJD_SJWul18", 0, false)]    // missing required field (isValid method return false)
        //[TestCase("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl", 1, false)]    // Pi toml for Pfh project
        //[TestCase("https://drive.google.com/open?id=1Ky3JyH1GwEzuduyZNaYXEM0l1LTn4Bbq", 0, false)]    // Pfh toml for Pi project
        //[TestCase("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl", 0, true)]    // Pi toml for Pi project
        //[TestCase("https://drive.google.com/open?id=1Ky3JyH1GwEzuduyZNaYXEM0l1LTn4Bbq", 1, true)]    // Pfh toml for Pfh project
        public async Task given_a_toml_url_is_the_project_succefuly_register(string url, int projectType, bool succefyllySaved)
        {
            (bool, string) retour;
            using(SqlStandardCallContext ctx = new SqlStandardCallContext())
            {
                ProjectStudentTable projectStudent = TestHelper.StObjMap.StObjs.Obtain<ProjectStudentTable>();
                SqlDefaultDatabase db = TestHelper.StObjMap.StObjs.Obtain<SqlDefaultDatabase>();
                CustomGroupTable groupTable = TestHelper.StObjMap.StObjs.Obtain<CustomGroupTable>();
                int memberId = new UserQueries(ctx, db).GetUserByEmail("agopian@intechinfo.fr").Result.UserId;

                retour = TomlHelpers.RegisterProject(url, projectType, projectStudent, memberId, db, groupTable).Result;
            }

            Console.WriteLine(retour.Item2);
            Assert.That(retour.Item1, Is.EqualTo(succefyllySaved));
        }
    }
}
