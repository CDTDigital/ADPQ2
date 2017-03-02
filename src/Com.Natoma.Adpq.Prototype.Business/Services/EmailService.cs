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
        private SmtpClient _client;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions;
        }

        /// <summary>
        /// Send single email async
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            if (_client == null)
            {
                _client = new SmtpClient();
                _client.Connect(_emailOptions.Value.SmtpServer, _emailOptions.Value.Port);
                _client.Authenticate(_emailOptions.Value.Username, _emailOptions.Value.Password);
            }
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_emailOptions.Value.FromName, _emailOptions.Value.FromAddress));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                var bodyBuilder = new BodyBuilder { HtmlBody = message };
                emailMessage.Body = bodyBuilder.ToMessageBody();
                    
                await _client.SendAsync(emailMessage).ConfigureAwait(false);
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// If a client is connected, disconnects.
        /// </summary>
        public void Disconnect()
        {
            if (_client != null && _client.IsConnected)
            {
                _client.Disconnect(true);
                _client.Dispose();
            }
        }
    }
}

