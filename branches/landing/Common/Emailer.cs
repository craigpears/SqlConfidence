using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common
{
    public class Emailer
    {
        SmtpClient _client;

        public Emailer()
        {
            _client = new SmtpClient();
            _client.Port = 25;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = true;
            _client.Credentials = new NetworkCredential("EmailUser", "Email55%%", "DEDICAT-LM03IFM");
            _client.Host = Properties.Settings.Default.SmtpHost;
        }

        public void SendResetEmail(Models.UserModel user, Guid token)
        {
            throw new NotImplementedException();
        }

        public void SendQuestionEmail(String UserEmail, String Question)
        {
            Question += " from " + UserEmail;
            MailMessage message = new MailMessage("noreply@sqlconfidence.com", "dachimpy36@googlemail.com", "Sql Confidence Question", Question);
            _client.Send(message);

            MailMessage message2 = new MailMessage("noreply@sqlconfidence.com", "mary.halpin9@gmail.com", "Sql Confidence Question", Question);
            _client.Send(message2);
        }
    }
}
