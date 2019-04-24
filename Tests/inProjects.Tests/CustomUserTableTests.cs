using CK.Core;
using CK.SqlServer;
using FluentAssertions;
using inProjects.Data;
using NUnit.Framework;
using System;


using static CK.Testing.DBSetupTestHelper;

namespace inProjects.Tests
{
    [TestFixture]
    class CustomUserTableTests
    {
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
    }
}
