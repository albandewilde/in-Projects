using NUnit.Common;
using NUnitLite;
using System;

namespace inProjects.TomlHelpers.Tests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return new AutoRun( typeof( Program ).Assembly ).Execute( args, new ExtendedTextWrapper( Console.Out ), Console.In );
        }
    }
}
