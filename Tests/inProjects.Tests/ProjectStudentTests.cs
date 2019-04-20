//using CK.SqlServer;
//using inProjects.Data;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using static CK.Testing.DBSetupTestHelper;


//namespace inProjects.Tests
//{
//    [TestFixture]
//    class ProjectStudentTests
//    {
//        [Test]
//        public async Task create_a_project()
//        {
//            var projectStudent = TestHelper.StObjMap.StObjs.Obtain<ProjectStudentTable>();

//            using( var ctx = new SqlStandardCallContext() )
//            {
//                var ProjectCreate = await projectStudent.CreateProjectStudent( ctx, 1, 1, 1, "a;b;c", "aaa", "okok", "wwww", 1, 'I' );
//                Assert.That( ProjectCreate > 0 );
//            }
//        }
//    }
//}
