using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;

namespace NewsDataFeed.Helpers
{
    public static class EmailService
    {
        public static void SendMail(string subject, string html, string from = "tester6543@yandex.com", string to = "tester7654@yandex.com",
            string username = "Tester6543", string password = "Tester123")
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            try
            {
                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(username, password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
