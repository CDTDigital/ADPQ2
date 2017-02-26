using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class EmailService: IEmailService
    {
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("California Notifications", "CalNotification@example.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                var bodyBuilder = new BodyBuilder { HtmlBody = message };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = "some.domain.com";
                    await client.ConnectAsync("smtp.need.server", 25, SecureSocketOptions.None).ConfigureAwait(false);
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
