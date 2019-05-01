using CK.Setup;
using CK.SqlServer.Setup;
using System;
using System.Collections.Generic;
using System.Text;

namespace inProjects.Data
{
    [SqlTable( "tAclType", Package = typeof( Package ) ), Versions( "1.0.0" )]

    public abstract class AclTypeTable  : CK.DB.Acl.AclType.AclTypeTable
    {
        void StObjConstruct(SchoolTable schoolTable, CustomGroupTable customGroupTable)
        {

        }

    }
}
