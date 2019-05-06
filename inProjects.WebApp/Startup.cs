using CK.AspNet.Auth;
using CK.DB.AspNet.Auth;
using CK.Text;
using WebApp.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using CK.Auth;
using System.Text;
using inProjects.WebApp.Authentication;
using System.Security.Claims;
using inProjects.WebApp.Controllers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using CK.DB.User.UserOidc;

namespace WebApp
{
    public class Startup
    {
        readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env )
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddSingleton<IWebFrontAuthLoginService, SqlWebFrontAuthLoginService>();
            services.AddSingleton<IAuthenticationTypeSystem, StdAuthenticationTypeSystem>();

            services.Configure<SpaOptions>(o =>
            {
                o.Host = Configuration["Spa:Host"];
            });

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( secretKey ) );

            services.AddAuthentication( WebFrontAuthOptions.OnlyAuthenticationScheme )
                //.AddOutlook( "Outlook", options =>
                //{
                //    options.SignInScheme = WebFrontAuthOptions.OnlyAuthenticationScheme;
                //    options.ClientId = "1012618945754-fi8rm641pdegaler2paqgto94gkpp9du.apps.googleusercontent.com";
                //    options.ClientSecret = "vRALhloGWbPs7PJ5LzrTZwkH";
                //    options.Events = new OAuthEventHandler();
                //} )
                .AddOpenIdConnect( options =>
                {
                    options.SignInScheme = WebFrontAuthOptions.OnlyAuthenticationScheme;
                    options.Authority = "https://login.microsoftonline.com/" + Configuration["Authentication:Outlook:TenantId"];
                    options.ClientId = Configuration["Authentication:Outlook:ClientId"];
                    options.ResponseType = OpenIdConnectResponseType.IdToken;
                    options.CallbackPath = "/auth/signin-callback";
                    options.SignedOutRedirectUri = "https://localhost:8080/";
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.Events.OnTicketReceived = c => c.WebFrontAuthRemoteAuthenticateAsync<IUserOidcInfo>( payload =>
                    {
                        payload.SchemeSuffix = "";
                        // Instead of "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
                        // Use standard System.Security.Claims.ClaimTypes.
                        payload.Sub = c.Principal.FindFirst( ClaimTypes.NameIdentifier ).Value;
                    } );
                } )
                .AddWebFrontAuth( options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours( 1 );
                    options.SlidingExpirationTime = TimeSpan.FromHours( 1 );
                } )
                .AddCookie();

            if( _env.IsDevelopment() )
            {
                NormalizedPath dllPath = Configuration["StObjMap:Path"];
                if( !dllPath.IsEmptyPath )
                {
                    var solutionPath = new NormalizedPath( AppContext.BaseDirectory ).RemoveLastPart( 4 );
                    dllPath = solutionPath.Combine( dllPath ).AppendPart( "CK.StObj.AutoAssembly.dll" );
                    File.Copy( dllPath, Path.Combine( AppContext.BaseDirectory, "CK.StObj.AutoAssembly.dll" ), overwrite: true );
                }
            }

            services.AddCKDatabase( "CK.StObj.AutoAssembly" );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors( c =>
                    c.SetIsOriginAllowed( host => true )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}
