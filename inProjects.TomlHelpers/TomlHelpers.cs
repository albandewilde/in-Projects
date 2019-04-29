using System;
using System.Collections.Generic;
using System.Text;
using Nett;

namespace inProject.TomlHelpers
{
    public class TomlHelpers
    {
        public static void GenerateClassFromTomlSchema(string tomlSchema)
        {
            throw new NotImplementedException();
        }

        public static T GetInstanceFromToml<T>(string toml)
        {
            return Toml.ReadString<T>(toml);
        }
    }
}
