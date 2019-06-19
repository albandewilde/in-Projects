using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.Data
{
    [SqlTable( "tEvaluates", Package = typeof( Package ))]
    [Versions( "1.0.0" )]
    public abstract class EvaluatesTable : SqlTable
    {
        void StObjConstruct( CustomGroupTable groupTable, ProjectStudentTable projectTable )
        {

        }

        [SqlProcedure( "sEvaluateProject" )]
        public abstract Task EvaluateProject( ISqlCallContext context, int juryId, int projectId, int grade, DateTime begDate );

        [SqlProcedure( "sEvaluateProject" )]
        public abstract Task EvaluateProject( ISqlCallContext context, int juryId, int projectId, int grade);
    }
}
