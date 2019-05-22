
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Data.ProjectStudent;
using System.Threading.Tasks;

namespace inProjects.Data
{
    [SqlTable( "tProjectStudent", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class ProjectStudentTable : SqlTable
    {
        void StObjConstruct( CK.DB.Actor.GroupTable groupTable, TimedUserTable timedUserTable , CK.DB.SqlCKTrait.CKTraitTable cKTraitTable)
        {

        }

        [SqlProcedure( "sCreateProjectStudent" )]
         public abstract Task<ProjectStudentStruct> CreateProjectStudent( ISqlCallContext context, int actorId, int zoneId, string name, int CKTraitContextId, string traitName, string logo, string slogan, string pitch, int timedLeaderId, int leaderId, string type );
    }
}
