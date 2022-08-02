using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Models.SMS;
using Moj.CMS.Shared.Notifications.Dispatchers.Email;
using Moj.CMS.Shared.Notifications.Dispatchers.SMS;
using Moj.CMS.Shared.Settings;
using NETCore.Encrypt;

namespace Moj.CMS.Infrastructure.Queries
{
    public class SettingsQueries : ISettingsQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;
        private readonly IMapper _mapper;
        private readonly PasswordAESEncryptionOptions _passwordAesEncryptionOptions;

        public SettingsQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator, IMapper mapper,
            IOptions<PasswordAESEncryptionOptions> passwordEncryptionOptions)
        {
            _queryBuilderCreator = queryBuilderCreator;
            _mapper = mapper;
            _passwordAesEncryptionOptions = passwordEncryptionOptions.Value;
        }

        public async Task<EmailSettingsDto> GetEmailSettingsAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var emailSettings = await queryBuilder.Query<EmailSettings>().FirstOrDefaultAsync(p => p.IsActive);
            if (emailSettings != null)
                emailSettings.Password = EncryptProvider.AESDecrypt(emailSettings.Password, _passwordAesEncryptionOptions.Key);
            return _mapper.Map<EmailSettingsDto>(emailSettings);
        }

        public async Task<SmsSettingsDto> GetSMSSettingsAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var smsSettings = await queryBuilder.Query<SMSSettings>().FirstOrDefaultAsync(p => p.IsActive);
            if (smsSettings != null)
                smsSettings.Password = EncryptProvider.AESDecrypt(smsSettings.Password, _passwordAesEncryptionOptions.Key);
            return _mapper.Map<SmsSettingsDto>(smsSettings);
        }
    }
}
