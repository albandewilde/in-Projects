using System;
using System.Collections.Generic;

namespace inProject.ParseToml
{
    // convert a string in toml format to a Dictionary
    public class ParseToml
    {
        static public Dictionary<string, object> ParsePi(string toml)
        {
            Nett.TomlTable shadowed_toml = Nett.Toml.ReadString(toml);

            // for technos
            Nett.TomlValue[] technos_tomlValue = ((shadowed_toml["technologies"] as Nett.TomlTable)["technologies"] as Nett.TomlArray).Items;
            string[] technos = new string[technos_tomlValue.Length];
            for(int idx = 0; idx < technos_tomlValue.Length; idx += 1) {technos[idx] = technos_tomlValue[idx].ToString();}

            // for members
            Nett.TomlValue[] members_tomlValue = ((shadowed_toml["team"] as Nett.TomlTable)["members"] as Nett.TomlArray).Items;
            string[] members = new string[members_tomlValue.Length];
            for(int idx = 0; idx < members_tomlValue.Length; idx += 1) {members[idx] = members_tomlValue[idx].ToString();}


            Dictionary<string, object> dico = new Dictionary<string, object>()
            {
                {"name", new Dictionary<string, string>()
                {
                    {"project_name", ((shadowed_toml["name"] as Nett.TomlTable)["project_name"] as Nett.TomlString).ToString()}
                }
                },

                {"semester", new Dictionary<string, object>()
                {
                    {"semester", int.Parse(((shadowed_toml["semester"] as Nett.TomlTable)["semester"] as Nett.TomlInt).ToString())},
                    {"sector", ((shadowed_toml["semester"] as Nett.TomlTable)["sector"] as Nett.TomlString).ToString()},
                }
                },

                {"technologies", new Dictionary<string, string[]>()
                {
                    {"technologies", technos}
                }
                },

                {"logo", new Dictionary<string, string>()
                {
                    {"url", ((shadowed_toml["logo"] as Nett.TomlTable)["url"] as Nett.TomlString).ToString()}
                }
                },

                {"slogan", new Dictionary<string, string>()
                {
                    {"slogan", ((shadowed_toml["slogan"] as Nett.TomlTable)["slogan"] as Nett.TomlString).ToString()}
                }
                },

                {"pitch", new Dictionary<string, string>()
                {
                    {"pitch", ((shadowed_toml["pitch"] as Nett.TomlTable)["pitch"] as Nett.TomlString).ToString()}
                }
                },

                {"team", new Dictionary<string, object>()
                {
                    {"leader", ((shadowed_toml["team"] as Nett.TomlTable)["leader"] as Nett.TomlString).ToString()},
                    {"members", members}
                }
                },

            };

            return dico;
        }
    }
}
