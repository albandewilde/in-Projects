using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.Data
{
    [SqlTable( "tUser", Package = typeof( Package ) ), Versions( "1.0.0" )]
    public abstract partial class EventSchoolTable : SqlTable
    {
        void StObjConstruct(CustomGroupTable groupTable)
        {

        }

        [SqlProcedure( "sCreateEventSchool" )]
        public abstract Task CreateEvent( ISqlCallContext ctx, string name, DateTime begDate, DateTime endDate, int schoolId, int actorId);


    }
}
