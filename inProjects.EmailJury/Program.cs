using MimeKit;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace inProjects.EmailJury
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonContent = File.ReadAllText("./appsettings.json");
            JObject identifiant = JObject.Parse(jsonContent);

            Emailer emailer = new Emailer((string)identifiant["gmail"]["address"], (string)identifiant["gmail"]["username"], (string)identifiant["gmail"]["password"], "smtp.gmail.com", 587);

            MimeMessage msg =  emailer.CreateMessage("psimon@intechinfo.fr", "Bonjour Paul", "Voici un e-mail envoyé par l'application In'Projects");

            emailer.SendMessage(msg);

            // Wait the time the async method for sent the email is finished
            Thread.Sleep(1000);
        }
    }
}
