namespace inProjects.Data.Data.ProjectStudent
{
    public class ProjectSheet
    {
        public string[] place;
        public string name;
        public string semester;
        public string sector;
        public string logo;
        public string slogan;
        public string pitch;
        public (string, string[]) team;

        public ProjectSheet(string[] place, string name, string semester, string sector, string logo, string slogan, string pitch, (string, string[]) team)
        {
            this.place = place;
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
            string[] place,
            string name,
            string semester,
            string sector,
            string logo,
            string slogan,
            string pitch,
            (string, string[]) team,
            string[] technos
        ): base(place, name, semester, sector, logo, slogan, pitch, team)
        {
            this.technos = technos;
        }
    }

    public class ProjectPfhSheet: ProjectSheet
    {
        public string background;
        public ProjectPfhSheet(
            string[] place,
            string name,
            string semester,
            string sector,
            string logo,
            string slogan,
            string pitch,
            (string, string[]) team,
            string background
        ): base(place, name, semester, sector, logo, slogan, pitch, team)
        {
            this.background = background;
        }
    }
}
