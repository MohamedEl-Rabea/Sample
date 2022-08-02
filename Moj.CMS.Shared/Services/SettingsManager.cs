using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Models.SMS;
using Moj.CMS.Shared.Notifications.Dispatchers.Email;
using Moj.CMS.Shared.Notifications.Dispatchers.SMS;
using Moj.CMS.Shared.Settings;
using Moj.CMS.Shared.Wrapper;
using NETCore.Encrypt;

namespace Moj.CMS.Shared.Services
{
    public class SettingsManager : ISettingsManager
    {
        private readonly IRepository<EmailSettings> _emailSettingsRepository;
        private readonly IRepository<SMSSettings> _smsSettingsRepository;
        private readonly PasswordAESEncryptionOptions _passwordAESEncryptionOptions;
        private readonly IMapper _mapper;

        public SettingsManager(IRepository<EmailSettings> emailSettingsRepository,
            IRepository<SMSSettings> smsSettingsRepository,
            IOptions<PasswordAESEncryptionOptions> passwordEncryptionOptions,
            IMapper mapper)
        {
            _emailSettingsRepository = emailSettingsRepository;
            _smsSettingsRepository = smsSettingsRepository;
            _passwordAESEncryptionOptions = passwordEncryptionOptions.Value;
            _mapper = mapper;
        }
        public async Task<IResult> SaveEmailSettingsAsync(EmailSettingsDto emailSettingsDto)
        {
            var emailSettings = _mapper.Map<EmailSettings>(emailSettingsDto);
            emailSettings.Password = EncryptProvider.AESEncrypt(emailSettings.Password, _passwordAESEncryptionOptions.Key);
            await _emailSettingsRepository.InsertOrUpdateAsync(emailSettings);
            await _emailSettingsRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<IResult> SaveSmsSettingsAsync(SmsSettingsDto smsSettingsDto)
        {
            var smsSettings = _mapper.Map<SMSSettings>(smsSettingsDto);
            smsSettings.Password = EncryptProvider.AESEncrypt(smsSettings.Password, _passwordAESEncryptionOptions.Key);
            await _smsSettingsRepository.InsertOrUpdateAsync(smsSettings);
            await _smsSettingsRepository.SaveChangesAsync();
            return Result.Success();
        }
    }
}
