
using CK.Setup;
using CK.SqlServer.Setup;


namespace inProjects.Data
{
    [SqlTable( "tProjectStudent", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class ProjectStudentTable : SqlTable
    {
        void StObjConstruct( CK.DB.Actor.GroupTable groupTable, CK.DB.Actor.UserTable userTable, CK.DB.SqlCKTrait.CKTraitTable cKTraitTable)
        {

        }
    }
}
