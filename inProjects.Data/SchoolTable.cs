using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;
using System.Threading.Tasks;

namespace inProjects.Data
{
    [SqlTable( "tSchool", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class SchoolTable : SqlTable
    {
        void StObjConstruct()
        {

        }

        //[SqlProcedure( "sCreateSchool" )]
        //public abstract Task<int> Create( ISqlCallContext ctx, string name );
    }
}
