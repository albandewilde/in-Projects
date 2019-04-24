using CK.Core;
using CK.SqlServer;
using FluentAssertions;
using inProjects.Data;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using static CK.Testing.DBSetupTestHelper;


namespace inProjects.Tests
{

    [TestFixture]

    class TimePeriodTests
    {
        [Test]
        public async Task create_period()
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime2 = DateTime.Now.AddDays(1);
            var timePeriod = TestHelper.StObjMap.StObjs.Obtain<TimePeriodTable>();
            using( var ctx = new SqlStandardCallContext(TestHelper.Monitor) )
            {
                var id = await timePeriod.CreateTimePeriodAsync( ctx, 1, dateTime, dateTime2, "I" );
                Assert.That( id > 0 );

            }

        }

        [Test]
        public async Task create_period_with_parent_zone()
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime2 = DateTime.Now.AddDays( 1 );
            var timePeriod = TestHelper.StObjMap.StObjs.Obtain<TimePeriodTable>();
            using( var ctx = new SqlStandardCallContext( TestHelper.Monitor ) )
            {
                var id = await timePeriod.CreateTimePeriodAsync( ctx, 1, dateTime, dateTime2, "I", 4 );
                Assert.That( id > 0 );

            }

        }


    }
}
