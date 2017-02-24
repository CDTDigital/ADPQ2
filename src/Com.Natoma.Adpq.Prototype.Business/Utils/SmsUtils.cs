using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Com.Natoma.Adpq.Prototype.Business.Utils
{
    public class SmsUtils
    {
        public bool SendSms()
        {
            // Your Account SID from twilio.com/console
            var accountSid = "ACc9873a96e6d9ee87f5bda07a0262e03b";
            // Your Auth Token from twilio.com/console
            var authToken = "9b7088a2b119932b4cf4048f257422a5";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber("+15558675309"),
                from: new PhoneNumber("+15017250604"),
                body: "Hello from C#");

            return true;
        }
    }
}
