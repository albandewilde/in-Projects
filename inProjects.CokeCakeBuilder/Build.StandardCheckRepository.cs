using System;
using System.Collections.Generic;
using System.Text;
using Cake.Common.Solution;
using SimpleGitVersion;
using CSemVer;
using System.Linq;
using Cake.Core;
using CodeCake;
using Cake.Common.Diagnostics;

namespace CodeCake
{
    public partial class Build
    {
        public class CheckRepositoryInfo
        {
            public bool IsLocalCIRelease;
            public string LocalFeedPath;
            public bool PushToRemote;
            public SimpleRepositoryInfo GitInfo { get; }
            public ICakeContext Cake { get; }
            public bool IgnoreNoPackagesToProduce { get; set; }
            public string BuildConfiguration { get; set; } = "Debug";

            public CheckRepositoryInfo( ICakeContext ctx, SimpleRepositoryInfo gitInfo )
            {
                GitInfo = gitInfo;
                Cake = ctx;
            }           
        }
        CheckRepositoryInfo StandardCheckRepository( SimpleRepositoryInfo gitInfo )
        {
            CheckRepositoryInfo result = new CheckRepositoryInfo( Cake, gitInfo );

            if( !gitInfo.IsValid )
            {
                if( Cake.InteractiveMode() != InteractiveMode.NoInteraction
                    && Cake.ReadInteractiveOption( "PublishDirtyRepo",
                        "Repository is not ready to be published. Proceed anyway?", 'Y', 'N' ) == 'Y' )
                {
                    Cake.Warning( "GitInfo is not valid, but you choose to continue..." );
                    result.IgnoreNoPackagesToProduce = true;
                }
                else
                {
                    Cake.TerminateWithError( "Repository is not ready to be published." );
                }
            }
            else
            {
                // gitInfo is valid: it is either ci or a release build. 
                // If a /LocalFeed/ directory exists above, we publish the packages in it.
                var localFeedRoot = Cake.FindDirectoryAbove( "LocalFeed" );
                if( localFeedRoot != null )
                {
                    var v = gitInfo.Info.FinalSemVersion;
                    if( v.AsCSVersion == null )
                    {
                        if( v.Prerelease.EndsWith( ".local" ) )
                        {
                            // Local releases must not be pushed on any remote and are copied to LocalFeed/Local
                            // feed (if LocalFeed/ directory above exists).
                            result.IsLocalCIRelease = true;
                            result.LocalFeedPath = System.IO.Path.Combine( localFeedRoot, "Local" );
                        }
                        else
                        {
                            // CI build versions are routed to LocalFeed/CI
                            result.LocalFeedPath = System.IO.Path.Combine( localFeedRoot, "CI" );
                        }
                    }
                    else
                    {
                        // Release or prerelease go to LocalFeed/Release
                        result.LocalFeedPath = System.IO.Path.Combine( localFeedRoot, "Release" );
                    }
                    System.IO.Directory.CreateDirectory( result.LocalFeedPath );
                }

                // Creating the right remote feed.
                if( !result.IsLocalCIRelease
                    && (Cake.InteractiveMode() == InteractiveMode.NoInteraction
                        || Cake.ReadInteractiveOption( "PushToRemote", "Push to Remote feeds?", 'Y', 'N' ) == 'Y') )
                {
                    result.PushToRemote = true;
                }
            }
            return result;
        }
    }
}
