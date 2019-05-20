using CK.Core;
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
        //[TestCase("this isn't a url", 0, false)]
        //[TestCase("http://example.com/", 0, false)]
        //[TestCase("https://drive.google.com/open?id=1Jce4M06bOOGLLrufoaMnfhOH30GopdXC", 0, false)]    // wrong file (faled to parse)
        //[TestCase("https://drive.google.com/open?id=1TQ5QMJ0QdB4VRPg3WdcxSBJD_SJWul18", 0, false)]    // missing required field (isValid method return false)
        //[TestCase("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl", 1, false)]    // Pi toml for Pfh project
        //[TestCase("https://drive.google.com/open?id=1Ky3JyH1GwEzuduyZNaYXEM0l1LTn4Bbq", 0, false)]    // Pfh toml for Pi project
        [TestCase("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl", 0, true)]    // Pi toml for Pi project
        //[TestCase("https://drive.google.com/open?id=1Ky3JyH1GwEzuduyZNaYXEM0l1LTn4Bbq", 1, true)]    // Pfh toml for Pfh project
        public async Task given_a_toml_url_is_the_project_succefuly_register(string url, int projectType, bool succefyllySaved)
        {
            (bool, string) retour;
            ProjectStudentTable projectStudent = TestHelper.StObjMap.StObjs.Obtain<ProjectStudentTable>();
            SqlDefaultDatabase db = TestHelper.StObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            CustomGroupTable groupTable = TestHelper.StObjMap.StObjs.Obtain<CustomGroupTable>();
            TimedUserTable userTimed = TestHelper.StObjMap.StObjs.Obtain<TimedUserTable>();

            // declare students
            string[] members = new string[]{"Julie Agopian", "Arthur Cheng", "Dan Chiche", "Melvin Delpierre", "Alban De Wilde"};
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

                // inserts students
                int idx = 0;
                foreach (string member in members)
                {
                    string[] name = member.Split();
                    int userId = await userTable.CreateUserAsync(ctx, 1, Guid.NewGuid().ToString(), name[0], name[1]);
                    membersId[idx] = userId;
                    TimedUserStruct timedUser = await userTimed.CreateOrUpdateTimedUserAsync( ctx, 1, periodId, userId );
                    await groupTable.AddUserAsync( ctx, 1, idGroup, userId );
                    idx += 1;
                }

                retour = TomlHelpers.RegisterProject(url, projectType, projectStudent, membersId[0], db, groupTable).Result;
            }

            Console.WriteLine(retour.Item2);
            Assert.That(retour.Item1, Is.EqualTo(succefyllySaved));
        }
    }
}
