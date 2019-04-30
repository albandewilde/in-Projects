using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using CodeCake;
using Cake.Core.Diagnostics;
using System.Linq;
using Cake.Core;
using Cake.Common.Solution;

namespace inProjects.CokeCakeBuilder
{
    public class Build : CodeCakeHost
    {
        public Build()
        {
            Cake.Log.Verbosity = Verbosity.Diagnostic;

            var configuration = "Debug";
            var solutionName = "inProjects";
            var solutionFileName = solutionName + ".sln";

            Task( "Clean" )
                .Does( () =>
                {
                    Cake.CleanDirectories( "**/bin/" + configuration, d => !d.Path.Segments.Contains( "CodeCakeBuilder" ) );
                    Cake.CleanDirectories( "**/obj/" + configuration, d => !d.Path.Segments.Contains( "CodeCakeBuilder" ) );
                } );

            Task( "Build" )
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

            Task( "Default" )
                .IsDependentOn( "Build" );
        }
    }
}
