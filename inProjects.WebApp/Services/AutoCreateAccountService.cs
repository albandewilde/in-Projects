using CK.AspNet.Auth;
using CK.Core;
using CK.DB.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inProjects.WebApp.Services
{
    public class AutoCreateAccountService : IWebFrontAuthAutoCreateAccountService
    {
        readonly UserTable _userTable;

        public Task<UserLoginResult> CreateAccountAndLoginAsync( IActivityMonitor monitor, IWebFrontAuthAutoCreateAccountContext context )
        {
            throw new NotImplementedException();
        }
    }
}
