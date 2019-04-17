using CK.Auth;
using CK.Core;
using CK.DB.Actor;
using CK.DB.Auth;
using CK.DB.Actor.ActorEMail;
using CK.SqlServer;
using inProjects.Data;
using inProjects.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly IOptions<SpaOptions> _spaOptions;
        readonly IAuthenticationTypeSystem _typeSystem;
        readonly IStObjMap _stObjMap;

        public AccountController( IOptions<SpaOptions> spaOptions, IAuthenticationTypeSystem typeSystem, IStObjMap stObjMap )
        {
            _spaOptions = spaOptions;
            _typeSystem = typeSystem;
            _stObjMap = stObjMap;
        }

        [HttpPost]
        [AllowAnonymous]
        public async void Register( [FromBody]RegisterModel model )
        {
            var userTable = _stObjMap.StObjs.Obtain<CustomUserTable>();
            var actorEmail = _stObjMap.StObjs.Obtain<ActorEMailTable>();
            var basic = _stObjMap.StObjs.Obtain<IBasicAuthenticationProvider>();

            using ( var ctx = new SqlStandardCallContext() )
            {
                var userId = await userTable.CreateUserAsync( ctx, 1,
                    Guid.NewGuid().ToString(), model.FirstName, model.LastName );

                if( userId != 0 && userId != 1)
                {
                    await basic.CreateOrUpdatePasswordUserAsync( ctx, 1, userId, model.Password );
                    await actorEmail.AddEMailAsync( ctx, 1, userId, model.Email, true, false );
                    LoginResult res = await basic.LoginUserAsync( ctx, userId, model.Password );
                }
            }
        }
    }
}
