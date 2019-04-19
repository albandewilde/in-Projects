using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Data.TimedUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.Data
{
    [SqlTable( "tTimedUser", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class TimedUserTable : SqlTable
    {
        void StObjConstruct( CK.DB.Actor.UserTable userTable, TimePeriodTable timePeriodTable)
        {

        }

        [SqlProcedure( "sCreateTimedUser" )]
        public abstract Task<TimedUserStruct> CreateOrUpdateTimedUserAsync( ISqlCallContext context, int typeUser, int timePeriodId, int userId);
    }
}
