
using CK.Setup;
using CK.SqlServer.Setup;
using System.Threading.Tasks;

namespace inProjects.Data
{
    [SqlTable( "tProjectStudent", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]
    public abstract class ProjectStudentTable : SqlTable
    {
        void StObjConstruct( CK.DB.Actor.GroupTable groupTable, CK.DB.Actor.UserTable userTable, CK.DB.SqlCKTrait.CKTraitTable cKTraitTable)
        {

        }

        [SqlProcedure( "sProjectStudentCreate" )]

        public abstract Task<int> CreateProjectStudent(int actorId, )


    }
}
