using System;
using System.Collections.Generic;
using System.Text;
using CK.DB.Actor;
using CK.Setup;
using CK.SqlServer.Setup;

namespace inProjects.Data
{
    [SqlTable("tUser", Package = typeof( Package ) ), Versions( "1.0.0" )]
    public abstract class CustomUserTable : UserTable
    {
        void StObjConstruct(UserTable userTable)
        {

        }
    }
}
