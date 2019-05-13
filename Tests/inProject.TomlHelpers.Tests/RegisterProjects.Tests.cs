using NUnit.Framework;

namespace inProjects.TomlHelpers.Tests
{
    [TestFixture]
    public class RegisterProjects
    {

        [TestCase("this isn't a url", 0, false)]
        [TestCase("http://example.com/", 0, false)]
        [TestCase("https://drive.google.com/open?id=1Jce4M06bOOGLLrufoaMnfhOH30GopdXC", 0, false)]    // wrong file (faled to parse)
        [TestCase("https://drive.google.com/open?id=1TQ5QMJ0QdB4VRPg3WdcxSBJD_SJWul18", 0, false)]    // missing required field (isValid method return false)
        [TestCase("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl", 1, false)]    // Pi toml for Pfh project
        [TestCase("https://drive.google.com/open?id=1Ky3JyH1GwEzuduyZNaYXEM0l1LTn4Bbq", 0, false)]    // Pfh toml for Pi project
        [TestCase("https://drive.google.com/open?id=1DgzQL3-7vmp1xptBH26H543e51eOYDLl", 0, true)]    // Pi toml for Pi project
        [TestCase("https://drive.google.com/open?id=1Ky3JyH1GwEzuduyZNaYXEM0l1LTn4Bbq", 1, true)]    // Pfh toml for Pfh project
        public void given_a_toml_url_is_the_project_succefuly_register(string url, int projectType, bool succefyllySaved)
        {
            (bool, string) retour = TomlHelpers.RegisterProject(url, projectType);
            Assert.That(retour.Item1, Is.EqualTo(succefyllySaved));
        }
    }
}
