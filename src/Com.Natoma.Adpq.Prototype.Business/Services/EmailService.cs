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

                emailMessage.From.Add(new MailboxAddress("ADPQ2", "adpq2@natomatech.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                var bodyBuilder = new BodyBuilder { HtmlBody = message };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("email-smtp.us-west-2.amazonaws.com", 587);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("AKIAIYHSSHMRLYEMH4JQ", "Ajw/wsncU3ppLhZIMCDjfR2uZqqkRM44mTe/ewx0BgNl");
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
