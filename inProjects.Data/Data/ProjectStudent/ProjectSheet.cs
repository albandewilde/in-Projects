namespace inProjects.Data.Data.ProjectStudent
{
    public class ProjectSheet
    {
        public string name;
        public string semester;
        public string sector;
        public string logo;
        public string slogan;
        public string pitch;
        public (string, string[]) team;

        public ProjectSheet(string name, string semester, string sector, string logo, string slogan, string pitch, (string, string[]) team)
        {
            this.name = name;
            this.semester = semester;
            this.sector = sector;
            this.logo = logo;
            this.slogan = slogan;
            this.pitch = pitch;
            this.team = team;
        }
    }

    public class ProjectPiSheet: ProjectSheet
    {
        public string[] technos;

        public ProjectPiSheet(
            string name,
            string semester,
            string sector,
            string logo,
            string slogan,
            string pitch,
            (string, string[]) team,
            string[] technos
        ): base(name, semester, sector, logo, slogan, pitch, team)
        {
            
            this.name = name;
            this.semester = semester;
            this.sector = sector;
            this.logo = logo;
            this.slogan = slogan;
            this.pitch = pitch;
            this.team = team;
            this.technos = technos;
        }
    }

    public class ProjectPfhSheet: ProjectSheet
    {
        public string background;
        public ProjectPfhSheet(
            string name,
            string semester,
            string sector,
            string logo,
            string slogan,
            string pitch,
            (string, string[]) team,
            string background
        ): base(name, semester, sector, logo, slogan, pitch, team)
        {
            this.name = name;
            this.semester = semester;
            this.sector = sector;
            this.logo = logo;
            this.slogan = slogan;
            this.pitch = pitch;
            this.team = team;
            this.background = background;
        }
    }
}
