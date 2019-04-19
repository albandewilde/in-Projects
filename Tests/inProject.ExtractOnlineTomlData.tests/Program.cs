using System;
using NUnitLite;
using NUnit.Common;

namespace inProject.ExtractOnlineTomlData.tests
{
    class Program
    {
        static int Main(string[] args)
        {
            return new AutoRun(typeof(Program).Assembly).Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
        }
    }
}