using CK.AspNet.Auth;
using CK.Auth;
using CK.Core;
using CK.DB.Actor;
using CK.DB.Actor.ActorEMail;
using CK.DB.Auth;
using CK.DB.User.UserOidc;
using CK.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.WebApp.Services
{
    public class AutoCreateAccountService : IWebFrontAuthAutoCreateAccountService
    {
        readonly UserTable _userTable;
        readonly ActorEMailTable _actorEmailTable;
        readonly UserOidcTable _oidcTable;
        readonly IAuthenticationDatabaseService _dbAuth;
        readonly IAuthenticationTypeSystem _typeSystem;

        public AutoCreateAccountService(
            UserTable userTable,
            ActorEMailTable actorEmail,
            UserOidcTable oidcTable,
            IAuthenticationDatabaseService dbAuth,
            IAuthenticationTypeSystem typeSystem)
        {
            _userTable = userTable;
            _actorEmailTable = actorEmail;
            _oidcTable = oidcTable;
            _dbAuth = dbAuth;
            _typeSystem = typeSystem;
        }

        public async Task<UserLoginResult> CreateAccountAndLoginAsync( IActivityMonitor monitor, IWebFrontAuthAutoCreateAccountContext context )
        {
            ISqlCallContext ctx = context.HttpContext.RequestServices.GetService<ISqlCallContext>();
            int userId = await _userTable.CreateUserAsync( ctx, 1, Guid.NewGuid().ToString() );
            await _actorEmailTable.AddEMailAsync( ctx, 1, userId, context.HttpContext.User.Identity.Name, true );
            UCLResult result = await _oidcTable.CreateOrUpdateOidcUserAsync(
                ctx, 1, userId, (IUserOidcInfo)context.Payload, UCLMode.CreateOrUpdate | UCLMode.WithActualLogin );

            if( result.OperationResult != UCResult.Created ) return null;
            return await _dbAuth.CreateUserLoginResultFromDatabase( ctx, _typeSystem, result.LoginResult );
        }
    }
}
