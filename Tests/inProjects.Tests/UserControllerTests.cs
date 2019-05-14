//using NUnit.Framework;
//using CK.Core;
//using System.Collections.Generic;
//using CK.SqlServer;
//using CK.SqlServer.Setup;
//using FluentAssertions;
//using static CK.Testing.DBSetupTestHelper;
//using inProjects.Data.Queries;
//using CK.Auth;
//using inProjects.Data.Data.Group;
//using inProjects.Data.Data.Period;
//using System.Linq;
//using inProjects.Data.Data.TimedUser;

//namespace inProjects.Tests
//{
//    [TestFixture]
//    class UserControllerTests
//    {
//        [Test]

//        public async void get_the_list_of_all_users_in_a_group_at_a_periodAsync()
//        {
//            var zone = TestHelper.StObjMap.StObjs.Obtain<TimedPeriodQueries>();
//            var group = TestHelper.StObjMap.StObjs.Obtain<GroupQueries>();
//            var timedUser = TestHelper.StObjMap.StObjs.Obtain<TimedUserQueries>();
//            var authInfo = TestHelper.StObjMap.StObjs.Obtain<IAuthenticationInfo>();
//            int userId = authInfo.ActualUser.UserId;

//            GroupData groupData = await group.GetIdSchoolByConnectUser( userId );
//            PeriodData periodData = await zone.GetLastPeriodBySchool( groupData.GroupId );
//            IEnumerable<GroupData> groupList = await group.GetAllGroupByPeriod( periodData.ZoneId );

//            List<GroupData> groupFinal = groupList.ToList();
//            IEnumerable<TimedStudentData> studentList = new List<TimedStudentData>();
//            for( int i = 0; i < groupFinal.Count; i++ )
//            {
//                studentList = await timedUser.GetAllStudentInfosByGroup( groupFinal[i].GroupId );

//            }

//            return studentList;
//        }

//    }

