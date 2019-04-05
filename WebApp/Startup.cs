using CK.AspNet.Auth;
using CK.Text;
using WebApp.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

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

            services.Configure<SpaOption>(o =>
            {
                o.Host = Configuration["Spa:Host"];
            });

            services.AddAuthentication( WebFrontAuthOptions.OnlyAuthenticationScheme )
                .AddWebFrontAuth( options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours( 1 );
                    options.SlidingExpirationTime = TimeSpan.FromHours( 1 );
                } );

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

            app.UseAuthentication();

            app.UseCors( c =>
                    c.SetIsOriginAllowed( host => true )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials() );

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
