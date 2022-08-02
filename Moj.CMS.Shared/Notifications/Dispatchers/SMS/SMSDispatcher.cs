using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moj.CMS.Shared.Models.SMS;
using Moj.CMS.Shared.Settings;
using NETCore.Encrypt;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Moj.CMS.Shared.Notifications.Dispatchers.SMS
{
    public class SMSDispatcher : ISMSDispatcher
    {
        private readonly PasswordAESEncryptionOptions _passwordAESEncryptionOptions;
        private readonly ISettingsQueries _settingsQueries;
        SmsSettingsDto _smsSettingsDto;

        public SMSDispatcher(IOptions<PasswordAESEncryptionOptions> passwordEncryptionOptions, ISettingsQueries settingsQueries)
        {
            _passwordAESEncryptionOptions = passwordEncryptionOptions.Value;
            _settingsQueries = settingsQueries;
        }
        public async Task DispatchAsync(SMSInput smsInput)
        {
            _smsSettingsDto = await _settingsQueries.GetSMSSettingsAsync();
            var userName = _smsSettingsDto.UserName;
            var password = EncryptProvider.AESDecrypt(_smsSettingsDto.Password, _passwordAESEncryptionOptions.Key);
            TwilioClient.Init(userName, password);
            foreach (var destinationPhoneNumber in smsInput.DestinationPhoneNumbers)
            {
                await MessageResource.CreateAsync(
                    from: new PhoneNumber(_smsSettingsDto.SourcePhoneNumber),
                    to: new PhoneNumber(destinationPhoneNumber),
                    body: smsInput.Message
                );
            }
        }
    }
}
