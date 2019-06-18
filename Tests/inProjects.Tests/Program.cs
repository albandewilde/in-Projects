using CK.Monitoring;
using NUnit.Common;
using NUnitLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace inProjects.Tests
{
    public class Program
    {
        public static int Main( string[] args )
        {
            int result = new AutoRun( Assembly.GetEntryAssembly() )
                .Execute( args, new ExtendedTextWrapper( Console.Out ), Console.In );
            GrandOutput.Default?.Dispose();
            return result;
        }
    }
}
