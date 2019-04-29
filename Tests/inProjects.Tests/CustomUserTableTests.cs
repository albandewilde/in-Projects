using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using FluentAssertions;
using inProjects.Data;
using inProjects.Data.Data.TimedUser;
using inProjects.Data.Queries;
using NUnit.Framework;
using System;
using System.Data;
using System.Threading.Tasks;
using static CK.Testing.DBSetupTestHelper;

namespace inProjects.Tests
{
    [TestFixture]
    class CustomUserTableTests { 
        [Test]

        public void create_user_without_name()
        {
            var userTable = TestHelper.StObjMap.StObjs.Obtain<CustomUserTable>();

            using( var ctx = new SqlStandardCallContext() )
            {
                var userId = userTable.CreateUser( ctx, 1, Guid.NewGuid().ToString() );
                userId.Should().BeGreaterOrEqualTo( 0 );
            }
        }

        [Test]

        public void create_user_with_name()
        {
            var userTable = TestHelper.StObjMap.StObjs.Obtain<CustomUserTable>();

            using( var ctx = new SqlStandardCallContext() )
            {
                var userId = userTable.CreateUser( ctx, 1, Guid.NewGuid().ToString(), "prenom", "nom de famille" );
                userId.Should().BeGreaterOrEqualTo( 0 );

            }
        }

        [Test]
        public async Task Add_TimedUser_AnyType() 
        {
            var userTimed = TestHelper.StObjMap.StObjs.Obtain<TimedUserTable>();
            var userTable = TestHelper.StObjMap.StObjs.Obtain<CustomUserTable>();
            var school = TestHelper.StObjMap.StObjs.Obtain<SchoolTable>();
            var timePeriod = TestHelper.StObjMap.StObjs.Obtain<TimePeriodTable>();
            var sqlDatabase = TestHelper.StObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            DateTime dateTime = DateTime.Now;
            DateTime dateTime2 = DateTime.Now.AddDays( 1 );
            int maxType = 4;

            using( var ctx = new SqlStandardCallContext( TestHelper.Monitor ) )
            {
                TimedUserQueries timedUserQueries = new TimedUserQueries( ctx, sqlDatabase);
                var idSchool = await school.CreateSchool( ctx, 1, Guid.NewGuid().ToString() );
                var userId = await userTable.CreateUserAsync( ctx, 1, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() );
                var id = await timePeriod.CreateTimePeriodAsync( ctx, 1, dateTime, dateTime2, "S", idSchool );

                for( int i = 0; i < maxType; i++ )
                {
                    TimedUserStruct result = await userTimed.CreateOrUpdateTimedUserAsync( ctx, i, id, userId );
                    Assert.That( result.Status == i );

                    //if( await timedUserQueries.GetTimedUser( userId ) == null )
                    //{
                    //    TimedUserStruct result = await userTimed.CreateTimedUserAsync( ctx, i, id, userId );
                    //    Assert.That( result.Status == i );
                    //}


                }


            }
        }

        [Test]

        public async Task add_or_update_note()
        {
            var userTable = TestHelper.StObjMap.StObjs.Obtain<CustomUserTable>();
            var school = TestHelper.StObjMap.StObjs.Obtain<SchoolTable>();
            var timePeriod = TestHelper.StObjMap.StObjs.Obtain<TimePeriodTable>();
            var sqlDatabase = TestHelper.StObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            var noteTable = TestHelper.StObjMap.StObjs.Obtain<TimedUserNoteProjectTable>();
            var projectStudent = TestHelper.StObjMap.StObjs.Obtain<ProjectStudentTable>();
            var userTimed = TestHelper.StObjMap.StObjs.Obtain<TimedUserTable>();

            DateTime dateTime = DateTime.Now;
            DateTime dateTime2 = DateTime.Now.AddDays( 1 );

            double grade = 10.5F;



            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries timedUserQueries = new ProjectQueries( ctx, sqlDatabase );

                var idSchool = await school.CreateSchool( ctx, 1, Guid.NewGuid().ToString() );
                var userId = await userTable.CreateUserAsync( ctx, 1, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() );
                var id = await timePeriod.CreateTimePeriodAsync( ctx, 1, dateTime, dateTime2, "S", idSchool );
                // 0 = Anon Create TimedUser
                TimedUserStruct result = await userTimed.CreateOrUpdateTimedUserAsync( ctx, 0, id, userId );
                var ProjectCreate = await projectStudent.CreateProjectStudent( ctx, 1, 2, 1, "a;b;c", "aaa", "okok", "wwww", result.TimedUserId, "I" );

                await noteTable.AddOrUpdateNote( ctx, result.TimedUserId, ProjectCreate.ProjectStudentId, grade );

                Assert.That( await timedUserQueries.GetProjectGradeSpecJury( ProjectCreate.ProjectStudentId, result.TimedUserId ) == grade );

                grade = 15;

                await noteTable.AddOrUpdateNote( ctx, result.TimedUserId, ProjectCreate.ProjectStudentId, grade );

                Assert.That( await timedUserQueries.GetProjectGradeSpecJury( ProjectCreate.ProjectStudentId, result.TimedUserId ) == grade );

            }
        }
    }
}
