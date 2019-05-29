using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace inProjects.EmailJury
{
    public class Emailer
    {
        string email;
        string username;
        string password;
        string smtpServerAddress;
        int port;

        public Emailer(string email, string username, string password, string smtpServerAddress, int port)
        {
            this.email = email;
            this.username = username;
            this.password = password;
            this.smtpServerAddress = smtpServerAddress;
            this.port = port;
        }

        public MimeMessage CreateMessage(string to, string subject, string content)
        {
            // make the e-mail message
            MimeMessage message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            message.From.Add(new MailboxAddress(this.email));
            message.Subject = subject;
            message.Body = new TextPart("plain"){Text = content};

            return message;
        }

        public async Task SendMessage(MimeMessage msg)
        {
            using (SmtpClient client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s,c,h,e) => true;

				client.Connect(this.smtpServerAddress, this.port, false);    // Never use Ssl
				client.Authenticate(this.username, this.password);

				await client.SendAsync(msg);

				client.Disconnect(true);
            }
        }
    }
}