using CK.Setup;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;

namespace inProjects.Data
{
    [SqlTable( "tTimePeriod", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class ForumInfosTable : SqlTable
    {
        void StObjConstruct( ProjectStudentTable projectStudentTable )
        {

        }
    }
}
