using System;
using System.IO;
using NUnit.Framework;



namespace inProject.ExtractOnlineTomlData.tests
{
    [TestFixture]
    public class ExtractOnlineTomlDataTypesTests
    {
        [Test]
        public void ExtractOnlineTomlData_test_MatchRequiredFieldAndTypes_method()
        {
            //dynamic types = (File.ReadAllText("./types.toml"));
            Nett.TomlTable toml = Nett.Toml.ReadFile("./sample.toml");
            //ExtractOnlineTomlData handler = new ExtractOnlineTomlData("test.toml", types);

            Console.WriteLine("¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤\n");

            System.Collections.Generic.ICollection<string> thing = toml.Keys;
            Console.WriteLine(thing.GetEnumerator());
            foreach (var elem in thing) {
                Console.WriteLine(elem);
            }

            Console.WriteLine(toml.Values);
            foreach (var elem in toml.Values) {
                Console.WriteLine("Content: ");
                Console.WriteLine(elem);
            }

            Console.WriteLine("THE FUCKING KEY AND VALUE");
            Console.WriteLine(toml["owner"]);
            Console.WriteLine("ONLY THE DUMB NAME");
            Nett.TomlTable THE_NAME_BITCH = toml["owner"] as Nett.TomlTable;
            Console.WriteLine(THE_NAME_BITCH["name"]);


            Console.WriteLine("\n¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
            //Assert.IsTrue(toml.owner.name is string);
            //Assert.AreEqual(toml.owner.name.GetType().Name, types.owner.name);
        }
    }
}