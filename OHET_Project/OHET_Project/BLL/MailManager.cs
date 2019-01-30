using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace OHET_Project.BLL
{
    public class MailManager
    {
        private string destination;

        public MailManager(string destination)
        {
            this.destination = destination;
        }

        public void SendUnbanMessage(string nick)
        {
            var topic = "Your account has been unbanned";
            var message = String.Format("Hello {0}!\n", nick);
            message += message += "Your account has been unbanned! \n";

            SendMail(message, topic);
        }

        public void SendBanMessage(string nick)
        {
            var topic = "Your account has been suspended!";
            var message = String.Format("Hello {0}!\n", nick);
            message += "Your account has been suspended! \n";
            message += "For more informations or if you think that ban was unfair appeal to this mail: ohetlegacy@gmail.com";

            SendMail(message, topic);
        }

        public void SendMail(string body, string topic)
        {
            MailMessage mail = new MailMessage("ohetlegacy@gmail.com", destination);

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new NetworkCredential("ohetlegacy@gmail.com", "Pjatk123");

            mail.Subject = topic;
            mail.Body = body;

            client.Send(mail);
        }

    }
}