using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class SmsService: ISmsService
    {
        public async Task<bool> SendSms(string phoneNumber, string smsMessage)
        {
            // Your Account SID from twilio.com/console
            // TODO: Test credentials not sending real messages
            var accountSid = "ACc9873a96e6d9ee87f5bda07a0262e03b";
            // Your Auth Token from twilio.com/console
            var authToken = "9b7088a2b119932b4cf4048f257422a5";

            try
            {
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    to: new PhoneNumber(phoneNumber),
                    from: new PhoneNumber("+15005550006"),
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
