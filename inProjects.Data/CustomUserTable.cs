
using System.Threading.Tasks;
using CK.DB.Actor;
using CK.Setup;
using CK.SqlServer;
using CK.SqlServer.Setup;

namespace inProjects.Data
{
    [SqlTable("tUser", Package = typeof( Package ) ), Versions( "1.0.0" )]
    
    public abstract partial class CustomUserTable : UserTable
    {
        void StObjConstruct(UserTable userTable)
        {

        }

        [SqlProcedure( "transform:sUserCreate" )]
        public abstract Task<int> CreateUserAsync( ISqlCallContext context, int actorId, string userName, string firstName, string lastName );

    }
}
