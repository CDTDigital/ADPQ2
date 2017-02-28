using System;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Options;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class EmailService: IEmailService
    {
        private readonly IOptions<EmailOptions> _emailOptions;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_emailOptions.Value.FromName, _emailOptions.Value.FromAddress));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                var bodyBuilder = new BodyBuilder { HtmlBody = message };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailOptions.Value.SmtpServer, _emailOptions.Value.Port);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailOptions.Value.Username, _emailOptions.Value.Password);
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
