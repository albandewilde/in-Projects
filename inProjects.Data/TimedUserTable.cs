using CK.Setup;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;

namespace inProjects.Data
{
    [SqlTable( "tTimedUser", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class TimedUserTable : SqlTable
    {
        void StObjConstruct( CK.DB.Actor.UserTable userTable, TimePeriodTable timePeriodTable)
        {

        }
    }
}
