using Cake.Common.Solution;
using Cake.Common.IO;
using Cake.Core;
using System.Linq;
using Cake.Core.Diagnostics;
using SimpleGitVersion;
using System.Collections.Generic;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore;
using System.IO.Compression;
using System.IO;

namespace CodeCake
{

    /// <summary>
    /// Sample build "script".
    /// Build scripts can be decorated with AddPath attributes that inject existing paths into the PATH environment variable. 
    /// </summary>
    [AddPath( "%UserProfile%/.nuget/packages/**/tools*" )]
    public partial class Build : CodeCakeHost
    {
        public Build()
        {
            Cake.Log.Verbosity = Verbosity.Diagnostic;

            const string solutionName = "inProjects";
            const string solutionFileName = solutionName + ".sln";

            var releasesDir = Cake.Directory( "CodeCakeBuilder/Releases" );

            var projects = Cake.ParseSolution( solutionFileName )
                                       .Projects
                                       .Where( p => !(p is SolutionFolder)
                                                     && p.Name != "CodeCakeBuilder" );

            // We publish .Tests projects for this solution.
            //var projectsToPublish = projects;

            // The SimpleRepositoryInfo should be computed once and only once.
            SimpleRepositoryInfo gitInfo = Cake.GetSimpleRepositoryInfo();
            // This default global info will be replaced by Check-Repository task.
            // It is allocated here to ease debugging and/or manual work on complex build script.
            CheckRepositoryInfo globalInfo = new CheckRepositoryInfo( Cake, gitInfo );

            Task( "Check-Repository" )
               .Does( () =>
               {
                   globalInfo = StandardCheckRepository( gitInfo );
               } );

            Task( "Clean" )
                .Does( () =>
                {
                    Cake.CleanDirectories( projects.Select( p => p.Path.GetDirectory().Combine( "bin" ) ) );
                    Cake.CleanDirectories( releasesDir );
                    Cake.DeleteFiles( "Tests/**/TestResult*.xml" );
                } );

            Task( "Build" )
                .IsDependentOn( "Check-Repository" )
                .IsDependentOn( "Clean" )
                .Does( () =>
                {
                    using( var tempSln = Cake.CreateTemporarySolutionFile( solutionFileName ) )
                    {
                        tempSln.ExcludeProjectsFromBuild( "CodeCakeBuilder" );
                        Cake.DotNetCoreBuild( tempSln.FullPath.FullPath, new DotNetCoreBuildSettings()
                        {
                            Configuration = "Debug",
                            Verbosity = DotNetCoreVerbosity.Minimal,
                            ArgumentCustomization = args => args.Append( "/p:GenerateDocumentation=true" )
                        } );

                        var t = System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetEntryAssembly().Location );
                        System.IO.DirectoryInfo d = new System.IO.DirectoryInfo( @"./" );//Assuming Test is your Folder
                        System.IO.FileInfo[] Files = d.GetFiles( "*" ); //Getting Text files
                    }
                } );

            Task( "Unit-Testing" )
                .IsDependentOn( "Build" )
                .WithCriteria( () => Cake.InteractiveMode() == InteractiveMode.NoInteraction
                                     || Cake.ReadInteractiveOption( "RunUnitTests", "Run Unit Tests?", 'Y', 'N' ) == 'Y' )
               .Does( () =>
               {
                   var testProjects = projects.Where( p => p.Name.EndsWith( ".Tests" ) );
                   StandardUnitTests( globalInfo, testProjects );
               } );

            Task( "Publish-WebApp" )
                .IsDependentOn( "Unit-Testing" )
                .Does( () =>
                {
                    Cake.DotNetCorePublish( projects.First( p => p.Name.Equals( "inProjects.WebApp" ) ).Name );
                } );

            Task( "Export-Artifact" )
                .IsDependentOn( "Publish-WebApp" )
                .Does( () =>
                {
                    // Create WebApp zip
                    string zipNamePathWebApp = "./webapp.zip";

                    if( File.Exists( zipNamePathWebApp ))
                        File.Delete( zipNamePathWebApp );

                    ZipFile.CreateFromDirectory( projects.Single( p => p.Name.Equals( "inProjects.WebApp" ) ).Path.GetDirectory().Combine( "bin/Debug/netcoreapp2.1/publish" ).ToString(), zipNamePathWebApp );

                    // Create SPA zip

                    System.Diagnostics.Process.Start( "CMD.exe", "npm run build --prefix .\\inProjects.SPA\\" );

                    string zipNamePathSPA = "./spa.zip";

                    if( File.Exists( zipNamePathSPA ) )
                        File.Delete( zipNamePathSPA );

                    ZipFile.CreateFromDirectory( "./inProjects.SPA/dist", zipNamePathSPA );
                } );

            Task( "Default" )
                .IsDependentOn( "Export-Artifact" );
        }
    }
}
