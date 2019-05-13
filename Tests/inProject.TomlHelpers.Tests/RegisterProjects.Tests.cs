using NUnit.Framework;

namespace inProjects.TomlHelpers.Tests
{
    [TestFixture]
    public class RegisterProjects
    {
        [Test]
        public void the_url_is_invalid()
        {
            (bool, string) retour = TomlHelpers.RegisterProject("this isn't a url");
            Assert.False(retour.Item1);
        }

        [Test]
        public void the_url_isnt_a_toml_file()
        {
            (bool, string) retour = TomlHelpers.RegisterProject("http://example.com/");
            Assert.False(retour.Item1);
        }

        [Test]
        public void failed_to_parse_the_toml()
        {
            (bool, string) retour = TomlHelpers.RegisterProject("https://drive.google.com/open?id=1Jce4M06bOOGLLrufoaMnfhOH30GopdXC");
            Assert.False(retour.Item1);
        }

        [Test]
        public void missing_field_in_the_toml()
        {
            (bool, string) retour = TomlHelpers.RegisterProject("https://drive.google.com/open?id=1Jce4M06bOOGLLrufoaMnfhOH30GopdXC");
            Assert.False(retour.Item1);
        }

        [Test]
        public void all_good_the_file_is_valid()
        {
            (bool, string) retour = TomlHelpers.RegisterProject("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl");
            Assert.True(retour.Item1);
        }
    }
}
