using CK.Auth;
using CK.AspNet.Auth;
using CK.Core;
using CK.DB.Auth;
using CK.DB.Actor.ActorEMail;
using CK.SqlServer;
using inProjects.Data;
using inProjects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        readonly IOptions<SpaOptions> _spaOptions;
        readonly IAuthenticationTypeSystem _typeSystem;
        readonly IStObjMap _stObjMap;
        readonly IWebFrontAuthLoginService _loginService;

        public AccountController( IOptions<SpaOptions> spaOptions, IAuthenticationTypeSystem typeSystem, IStObjMap stObjMap, IWebFrontAuthLoginService loginService )
        {
            _spaOptions = spaOptions;
            _typeSystem = typeSystem;
            _stObjMap = stObjMap;
            _loginService = loginService;
        }

        [HttpPost ( "register" ) ]
        [AllowAnonymous]
        public async Task<string> Register( [FromBody]RegisterModel model )
        {
            var userTable = _stObjMap.StObjs.Obtain<CustomUserTable>();
            var actorEmail = _stObjMap.StObjs.Obtain<ActorEMailTable>();
            var basic = _stObjMap.StObjs.Obtain<IBasicAuthenticationProvider>();

            using ( var ctx = new SqlStandardCallContext() )
            {
                var loginId = Guid.NewGuid().ToString();
                var userId = await userTable.CreateUserAsync( ctx, 1,
                    loginId, model.FirstName, model.LastName );

                if( userId != 0 && userId != 1)
                {
                    await basic.CreateOrUpdatePasswordUserAsync( ctx, 1, userId, model.Password );
                    await actorEmail.AddEMailAsync( ctx, 1, userId, model.Email, true, false );

                    return loginId;
                }
                return null;
            }
        }

        [HttpPost ( "getUserName" ) ]
        [AllowAnonymous]
        public async Task<UserInfosModel> GetUserName([FromBody] LoginModel model)
        {
            var userTable = _stObjMap.StObjs.Obtain<CustomUserTable>();

            try
            {
                return await userTable.GetUserName( new SqlStandardCallContext(), model.Email );
            }
            catch( Exception e )
            {
                return null;
            }                      
        }       
    }
}
