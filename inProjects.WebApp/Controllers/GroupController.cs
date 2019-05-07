using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.Queries;
using inProjects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    public class GroupController : Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public GroupController( IStObjMap stObjMap, IAuthenticationInfo authenticationInfo )
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }

        [HttpGet("getAllGroupTemplate")]
        public async Task<List<GroupPeriod>> GetAllGroupTemplate()
        {
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                GroupQueries groupQueries = new GroupQueries( ctx, sqlDataBase );

                List<string> listGroups = await groupQueries.GetAllGroupByZoneId( 4 );

                List<GroupPeriod> list = new List<GroupPeriod>();

                for( int i = 0; i < listGroups.Count; i++ )
                {
                    GroupPeriod gp = new GroupPeriod();
                    gp.Name = listGroups[i];
                    gp.State = true;
                    gp.IsAlreadyPermanent = true;
                    list.Add( gp );
                }

                return list;
            }
        }
    }
}
