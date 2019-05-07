using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Nett;

namespace inProjects.TomlHelpers
{
    public static class TomlHelpers
    {
        public static T GetInstanceFromToml<T>(string toml)
        {
            return Toml.ReadString<T>(toml);
        }

        public static string GetOnlineToml(string url)
        {
            string str = new WebClient().DownloadString(url);
            return str;
        }
    }

    // class project
    public class Project
    {
        public Name name {get; set;}
        public Semester semester {get; set;}
        public Logo logo {get; set;}
        public Slogan slogan {get; set;}
        public Pitch pitch {get; set;}
        public Team team {get; set;}
        public OthersDocuments othersDocuments {get; set;}

        public bool isValid()
        {
            // properties that are optional, they can be null
            string[] optionalProperties = new string[]{"othersDocuments"};

            foreach (PropertyInfo propertie in this.GetType().GetProperties())
            {
                object propertieValue = propertie.GetValue(this, null);

                if (propertieValue is null && !optionalProperties.Contains(propertie.Name)) return false;
                if (
                    propertieValue is IProjectField //&&
                    /*(
                        // an optional propertie can be null, we check it here
                        propertieValue != null &&
                        optionalProperties.Contains(propertie.Name)
                    )*/
                ) if (!(propertieValue as IProjectField).isValid()) {
                    return false;
                }
            }
            return true;
        }
    }

    public class ProjectPi : Project
    {
        public Technologies technologies {get; set;}
    }

    public class ProjectPfh : Project
    {
        public Background background {get; set;}
    }

    // class of fields in project
    interface IProjectField
    {
        bool isValid();
    }

    public class Name: IProjectField
    {
        public string project_name {get; set;}
        public bool isValid(){return this.project_name != null && this.project_name != "";} 

    }

    public class Semester: IProjectField
    {
        public int semester {get; set;}
        public string sector {get; set;}

        public bool isValid()
        {
            return new int[]{1, 2, 3, 4, 5}.Contains(this.semester) && new string[]{"IL", "SR", "IL - SR", "None"}.Contains(this.sector);
        }
    }

    public class Logo: IProjectField
    {
        public string url {get; set;}
        public bool isValid(){return this.url != null && this.url != "";} 
    }

    public class Slogan: IProjectField
    {
        public string slogan {get; set;}
        public bool isValid(){return this.slogan != null && this.slogan != "";} 
    }

    public class Pitch: IProjectField
    {
        public string pitch {get; set;}

        public bool isValid(){return this.pitch != null && this.pitch != "";} 
    }

    public class Team: IProjectField
    {
        public string leader {get; set;}
        public string[] members {get; set;}

        public bool isValid()
        {
            // the leader isn't renseigned
            if (leader == null || leader == "" || members == null) {return false;}
            if (leader == "None")    // there is no leader
            {
                // the list member must have people in it
                if (this.members == null || this.members.Length == 0){return false;}
                // people in the list can't be null or empty string
                foreach (string member in members){if (member == null || member == "") return false;}
                return true;
            }
            else    // there is a leader
            {
                foreach (string member in members){if (member == null || member == "") return false;}
                return true;
            }
                
        }
    }

    public class OthersDocuments: IProjectField
    {
        public string[] urls {get; set;}

        public bool isValid()
        {
            if (this.urls == null || this.urls.Length <= 0) return false;
            foreach (string url in this.urls){if (url is null || url is "") return false;}

            return true;
        }
    }

    public class Technologies: IProjectField
    {
        public string[] technologies {get; set;}

        public bool isValid()
        {
            // the list have elements in it
            if (this.technologies == null || this.technologies.Length <= 0){return false;}
            // no empty element
            foreach (string techno in technologies){if (techno == null || techno == "") return false;}

            return true;
        }
    }

    public class Background: IProjectField
    {
        public string image {get; set;}

        public bool isValid() {return this.image != null && this.image != "";}
    }
}