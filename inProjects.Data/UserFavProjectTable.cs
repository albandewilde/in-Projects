using CK.Setup;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace inProjects.Data
{
    [SqlTable( "tUserFavProject", Package = typeof( Package ) )]
    [Versions( "1.0.0" )]

    public abstract class UserFavProjectTable : SqlTable
    {
        void StObjConstruct( CK.DB.Actor.UserTable userTable, ProjectStudentTable projectStudentTable)
        {

        }
    }
}
