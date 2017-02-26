using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Options;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class SmsService: ISmsService
    {
        private readonly IOptions<SmsOptions> _smsOptions;

        public SmsService(IOptions<SmsOptions> smsOptions)
        {
            _smsOptions = smsOptions;
        }

        public async Task<bool> SendSms(string phoneNumber, string smsMessage)
        {
            var accountSid = _smsOptions.Value.AccountSid;
            var authToken = _smsOptions.Value.AuthToken;

            try
            {
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    to: new PhoneNumber("+1" + phoneNumber.Replace("(","").Replace(")","").Replace("-","")),
                    from: new PhoneNumber(_smsOptions.Value.AccountPhone),
                    body: smsMessage);

                return true;
            }
            catch (Exception ex)
            {
                var stuff = ex.Message;
                return false;
            }
            
        }
    }
}
