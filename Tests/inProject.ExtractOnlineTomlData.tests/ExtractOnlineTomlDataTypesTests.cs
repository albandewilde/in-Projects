using System;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;


namespace inProject.ExtractOnlineTomlData.tests
{
    [TestFixture]
    public class ExtractOnlineTomlDataTypesTests
    {
        [Test]
        public void ExtractOnlineTomlData_test_MatchRequiredFieldAndTypes_method()
        {
            Dictionary<string, object> toml = ParseToml.ParseToml.ParsePi(File.ReadAllText("toml_sample_for_tests/pi_given_pattern.toml"));

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

            Assert.AreEqual(toml, dico);
        }
    }
}