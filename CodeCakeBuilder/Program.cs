using System;
using System.Linq;

namespace CodeCake
{
    class Program
    {
        const string SolutionDirectoryIsCurrentDirectoryParameter = "SolutionDirectoryIsCurrentDirectory";

        static int Main( string[] args )
        {
            string solutionDirectory = args.Contains( SolutionDirectoryIsCurrentDirectoryParameter, StringComparer.OrdinalIgnoreCase )
                ? Environment.CurrentDirectory
                : null;
            
            var app = new CodeCakeApplication( solutionDirectory );
            RunResult result = app.Run( args.Where( a => !StringComparer.OrdinalIgnoreCase.Equals( a, SolutionDirectoryIsCurrentDirectoryParameter ) ) );
            if( result.InteractiveMode == InteractiveMode.Interactive )
            {
                Console.WriteLine();
                Console.WriteLine( $"Hit any key to exit." );
                Console.WriteLine( $"Use -{InteractiveAliases.NoInteractionArgument} or -{InteractiveAliases.AutoInteractionArgument} parameter to exit immediately." );
                Console.ReadKey();
            }
            return result.ReturnCode;
        }
    }
}
