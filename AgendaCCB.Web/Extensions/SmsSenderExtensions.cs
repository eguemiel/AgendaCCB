using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio;

namespace AgendaCCB.Web.Extensions
{
    public static class SmsSenderExtensions
    {
        const string accountSid = "ACe7cb9bd3c2926da7bea2de94f651ed95";
        const string authToken = "fd652c87e65cb334260b716fbb52800d";
        const string fromPhoneNumber = "+19477770461";

        public static void SendMessage(string phoneNumber, string token)
        {
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(string.Format("+55{0}", phoneNumber));
            try
            {
                var message = MessageResource.Create(
                to,
                from: new PhoneNumber(fromPhoneNumber),
                body: string.Format("AGENDA CCB\n Numero telefone: {0}\n Codigo acesso: {1}.", phoneNumber, token),
                smartEncoded: true);
            }
            catch (Exception ex)
            {
                throw;
            }
                        
        }
    }
}
