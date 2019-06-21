using Cake.Common.Solution;
using Cake.Common.IO;
using Cake.Core;
using System.Linq;
using Cake.Core.Diagnostics;
using SimpleGitVersion;
using System.Collections.Generic;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore;

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

            Task( "Default" )
                .IsDependentOn( "Unit-Testing" );
        }
    }
}
