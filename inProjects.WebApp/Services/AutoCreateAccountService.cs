using CK.AspNet.Auth;
using CK.Auth;
using CK.Core;
using CK.DB.Actor.ActorEMail;
using CK.DB.Auth;
using CK.DB.User.UserOidc;
using CK.SqlServer;
using inProjects.Data;
using System;
using System.Threading.Tasks;

namespace inProjects.WebApp.Services
{
    public class AutoCreateAccountService : IWebFrontAuthAutoCreateAccountService
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationDatabaseService _dbAuth;
        readonly IAuthenticationTypeSystem _typeSystem;

        public AutoCreateAccountService(
            IAuthenticationDatabaseService dbAuth,
            IStObjMap stObjMap,
            IAuthenticationTypeSystem typeSystem)
        {
            _stObjMap = stObjMap;
            _dbAuth = dbAuth;
            _typeSystem = typeSystem;
        }

        public async Task<UserLoginResult> CreateAccountAndLoginAsync( IActivityMonitor monitor, IWebFrontAuthAutoCreateAccountContext context )
        {
            var userTable = _stObjMap.StObjs.Obtain<CustomUserTable>();
            var oidcTable = _stObjMap.StObjs.Obtain<UserOidcTable>();

            IUserOidcInfo infos = (IUserOidcInfo)context.Payload;
            ISqlCallContext ctx = context.HttpContext.RequestServices.GetService<ISqlCallContext>();

            int userId = await userTable.CreateUserAsync( ctx, 1, Guid.NewGuid().ToString() );            
            UCLResult result = await oidcTable.CreateOrUpdateOidcUserAsync(
                ctx, 1, userId, infos, UCLMode.CreateOrUpdate | UCLMode.WithActualLogin );
            if( result.OperationResult != UCResult.Created ) return null;
            return await _dbAuth.CreateUserLoginResultFromDatabase( ctx, _typeSystem, result.LoginResult );
        }
    }
}
