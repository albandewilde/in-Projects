using System;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
using inProject.TomlObjectVisitor;


namespace inProject.TomlObjectVisitor.Tests
{
    [TestFixture]
    public class SystemTypeFromTomlTypeTest
    {
        private GetSystemType getTypeVisitor = new GetSystemType();
        private Dictionary<string, object> toml = Nett.Toml.ReadFile("./toml_sample_for_tests/pi_given_pattern.toml").ToDictionary();


        [Test]
        public void SystemTypeFromTomlType_visit_with_TomlString_we_will_get_a_string_out_parameter()
        {
            Dictionary<string, object> dico = new Dictionary<string, object>()
            {
                {"name", new Dictionary<string, string>()
                {
                    {"project_name", "mon super projet"}
                }
                },

                {"semester", new Dictionary<string, object>()
                {
                    {"semester", 2},
                    {"sector", "SR"}
                }
                },

                {"technologies", new Dictionary<string, string[]>()
                {
                    {"technologies", new string[3]{"C#", "Kotlin", "Javascript"}}
                }
                },

                {"logo", new Dictionary<string, string>()
                {
                    {"url", "https://mon_super_logo.png"}
                }
                },

                {"slogan", new Dictionary<string, string>()
                {
                    {"slogan", "the super project"}
                }
                },

                {"pitch", new Dictionary<string, string>()
                {
                    {"pitch", "Vous avez des problemes ? Notre super project est là pour vous !"}
                }
                },

                {"team", new Dictionary<string, object>()
                {
                    {"leader", "Chef de Projet"},
                    {"members", new string[2]{"Team member one", "Team member two"}}
                }
                },

            };
            Assert.AreEqual(toml.Keys, dico.Keys);
        }

        class Person
        {
            public int age {get; set;}
            public Name name {get; set;}
        }
        class Name
        {
            public string first {get; set;}
        }

        [Test]
        public void how_TOML_to_class_work()
        {
            Person p = Nett.Toml.ReadFile<Person>("./toml_sample_for_tests/person.toml");
            Person n = new Person();
            n.age = 12;
            Name m = new Name();
            m.first = "Paul";
            n.name = m;

            Assert.AreEqual(p.age, n.age);
            Assert.AreEqual(p.name.first, n.name.first);
        }
    }

}