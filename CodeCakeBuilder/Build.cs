using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core.Diagnostics;
using System.Linq;
using Cake.Core;
using Cake.Common.Solution;
using SimpleGitVersion;
using System;

namespace CodeCake
{
    public partial class Build : CodeCakeHost
    {
        public Build()
        {
            Cake.Log.Verbosity = Verbosity.Diagnostic;

            var configuration = "Debug";
            var solutionName = "inProjects";
            var solutionFileName = solutionName + ".sln";

            var projects = Cake.ParseSolution( solutionFileName )
                .Projects
                .Where( p => !(p is SolutionFolder) && p.Name != "CodeCakeBuilder" );

            SimpleRepositoryInfo gitInfo = Cake.GetSimpleRepositoryInfo();
            CheckRepositoryInfo globalInfo = new CheckRepositoryInfo( Cake, gitInfo );

            Task( "Check-Repository" )
                .Does( () =>
                {
                    globalInfo = StandardCheckRepository( gitInfo );                    
                } );

            Task( "Clean" )
                .Does( () =>
                {
                    Cake.CleanDirectories( "**/bin/" + configuration, d => !d.Path.Segments.Contains( "CodeCakeBuilder" ) );
                    Cake.CleanDirectories( "**/obj/" + configuration, d => !d.Path.Segments.Contains( "CodeCakeBuilder" ) );
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
                            Configuration = configuration,
                            Verbosity = DotNetCoreVerbosity.Minimal,
                            ArgumentCustomization = args => args.Append( "/p:GenerateDocumentation=true" )
                        } );
                    }
                } );

            Task( "Unit-Testing" )
                .IsDependentOn( "Build" )
                .Does( () =>
                {
                    StandardUnitTests( globalInfo, projects.Where( p => p.Name.EndsWith( ".Tests" ) ) );
                } );

            Task( "Default" )
                .IsDependentOn( "Unit-Testing" );
        }
    }
}
