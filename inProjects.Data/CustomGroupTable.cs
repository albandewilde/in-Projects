using CK.DB.Zone;
using CK.Setup;
using CK.SqlServer.Setup;

namespace inProjects.Data
{

    [SqlTable( "tGroup", Package = typeof( Package ) ), Versions( "1.0.0" )]

    public abstract class CustomGroupTable : GroupTable
    {
        void StObjConstruct( GroupTable groupTable )
        {

        }
    }
}
