using System.IO;
using NUnit.Framework;

namespace inProjects.TomlHelpers.Tests
{
    [TestFixture]
    public class IsValidProjectsAndProjectsField
    {

        [TestCase("thing", true)]
        [TestCase("", false)]
        [TestCase("4", true)]
        [TestCase("THIS THE CUTEST DAY OF MY LIFE !!", true)]
        [TestCase("https://i.pinimg.com/originals/0f/e1/dd/0fe1dd92d58f600664be7045d5a4d5f9.jpg", true)]
        [TestCase(null, false)]
        public void background_valid_method(string str, bool expected)
        {
          Background bg = new Background();
          bg.image = str;

          Assert.That(bg.isValid(), Is.EqualTo(expected));
        }

        [TestCase(new string[]{"Python", "Rust", "Kotlin"}, true)]
        [TestCase(new string[]{}, false)]
        [TestCase(new string[]{"Bash", null, "Lua"}, false)]
        [TestCase(new string[]{"Bash", "null", ""}, false)]
        [TestCase(null, false)]
        public void technologies_valid_method(string[] technos, bool expected)
        {
            Technologies technologies = new Technologies();
            technologies.technologies = technos;

            Assert.That(technologies.isValid(), Is.EqualTo(expected));
        }

        [TestCase("Tom", new string[]{"Python", "Rust", "Kotlin"}, true)]
        [TestCase("Tom", new string[]{}, true)]
        [TestCase("Tom", new string[]{"Bash", null, "Lua"}, false)]
        [TestCase("Tom", new string[]{"Bash", "null", ""}, false)]
        [TestCase("Tom", null, false)]

        [TestCase("Tom", new string[]{"Python", "Rust", "Kotlin"}, true)]
        [TestCase("None", new string[]{"Python", "Rust", "Kotlin"}, true)]
        [TestCase("", new string[]{"Python", "Rust", "Kotlin"}, false)]
        [TestCase(null, new string[]{"Python", "Rust", "Kotlin"}, false)]

        [TestCase("", new string[]{}, false)]
        public void team_valid_method(string leader, string[] members, bool expected)
        {
            Team team = new Team();
            team.leader = leader;
            team.members = members;

            Assert.That(team.isValid(), Is.EqualTo(expected));
        }

        [TestCase("Vous avez des problemes ? Notre projet reponds à votre problème en vous en causant d'autre.", true)]
        [TestCase("", false)]
        [TestCase("4", true)]
        [TestCase("Ah Shit, Here We Go Again", true)]
        [TestCase("https://imgs.xkcd.com/comics/tests.png", true)]
        [TestCase(null, false)]
        public void pitch_valid_method(string ptch, bool expected)
        {
            Pitch pitch = new Pitch();
            pitch.pitch = ptch;

            Assert.That(pitch.isValid(), Is.EqualTo(expected));
        }

        [TestCase("Our project is better because we made it.", true)]
        [TestCase("", false)]
        [TestCase("12", true)]
        [TestCase("Yes, This is Dog", true)]
        [TestCase("A computer once beat me at chess, but it was no match for me at kick boxing.", true)]
        [TestCase(null, false)]
        public void slogan_valid_method(string slg, bool expected)
        {
            Slogan slogan = new Slogan();
            slogan.slogan = slg;

            Assert.That(slogan.isValid(), Is.EqualTo(expected));
        }

        [TestCase("https://assets.gentoo.org/tyrian/site-logo.png", true)]
        [TestCase("", false)]
        [TestCase("82", true)]
        [TestCase("A computer makes it possible to do, in half an hour, tasks which were completely unnecessary to do before.", true)]
        [TestCase("Computers aren't intelligent, they only think they are.", true)]
        [TestCase(null, false)]
        public void logo_valid_method(string l, bool expected)
        {
            Logo logo = new Logo();
            logo.url = l;

            Assert.That(logo.isValid(), Is.EqualTo(expected));
        }

        [TestCase(0, "IL", false)]
        [TestCase(2, "IR", false)]
        [TestCase(6, "SR", false)]
        [TestCase(null, "SR", false)]
        [TestCase(5, "", false)]
        [TestCase(3, "SR/IL", false)]
        [TestCase(2, null, false)]
        [TestCase(2, "SR", true)]
        [TestCase(5, "IL - SR", true)]
        [TestCase(1, "None", true)]
        public void semester_valid_method(int sem, string sector, bool expected)
        {
            Semester semester = new Semester();
            semester.semester = sem;
            semester.sector = sector;

            Assert.That(semester.isValid(), Is.EqualTo(expected));
        }

        [TestCase("The project name", true)]
        [TestCase("", false)]
        [TestCase("56", true)]
        [TestCase("I do not fear computers. I fear the lack of them.", true)]
        [TestCase("All computers wait at the same speed.", true)]
        [TestCase(null, false)]
        public void name_valid_method(string the_name, bool expected)
        {
            inProjects.TomlHelpers.Name name = new inProjects.TomlHelpers.Name();
            name.project_name = the_name;

            Assert.That(name.isValid(), Is.EqualTo(expected));
        }

        [TestCase("./toml_sample_for_tests/pi_given_pattern.toml", true)]
        [TestCase("./toml_sample_for_tests/wrong_semester_sector_pi_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/missing_field_technologies_pi_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/pfh_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/missing_field_name_projectname_pi_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/ficheprojet_in-projects.toml", true)]
        public void projectPi_valid_method(string filePath, bool expected)
        {
            string project_toml = File.ReadAllText(filePath);
            ProjectPi project = TomlHelpers.GetInstanceFromToml<ProjectPi>(project_toml);

            Assert.That(project.isValid(), Is.EqualTo(expected));
        }

        [TestCase("./toml_sample_for_tests/pfh_given_pattern.toml", true)]
        [TestCase("./toml_sample_for_tests/wrong_team_members_pfh_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/missing_field_pitch_pfh_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/pi_given_pattern.toml", false)]
        [TestCase("./toml_sample_for_tests/missing_field_logo_url_pfh_given_pattern.toml", false)]
        public void projectPfh_alid_method(string filepath, bool expected)
        {
            string project_toml = File.ReadAllText(filepath);
            ProjectPfh project = TomlHelpers.GetInstanceFromToml<ProjectPfh>(project_toml);

            Assert.That(project.isValid(), Is.EqualTo(expected));
        }
    }
}