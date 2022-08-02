using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Constants.User;
using Moj.CMS.Shared.Infrastructure.Seed;
using Moj.CMS.Shared.Notifications.Dispatchers.Email;
using Moj.CMS.Shared.Notifications.Dispatchers.SMS;
using Moj.CMS.Shared.Settings;
using NETCore.Encrypt;

namespace Moj.CMS.Infrastructure.Seed
{
    public class SettingsSeeder : IDatabaseSeeder
    {
        private readonly CMSDbContext _db;
        private readonly PasswordAESEncryptionOptions _passwordAESEncryptionOptions;

        public SettingsSeeder(CMSDbContext db, IOptions<PasswordAESEncryptionOptions> passwordEncryptionOptions)
        {
            _db = db;
            _passwordAESEncryptionOptions = passwordEncryptionOptions.Value;
        }

        public async Task SeedAsync()
        {
            if (!_db.EmailSettings.Any(e => e.EmailHost == "smtp.gmail.com" && e.IsActive == true))
            {
                await _db.EmailSettings.AddAsync(new EmailSettings
                {
                    EmailHost = "smtp.gmail.com",
                    Port = 465,
                    UseSsl = true,
                    UserName = "NoorHesham0707@gmail.com",
                    Password = EncryptProvider.AESEncrypt("PP@@ssww00rrdd123", _passwordAESEncryptionOptions.Key),
                    DisplayName = "Nourhan Hesham",
                    IsActive = true
                });
                await _db.SaveChangesAsync();
            }
            if (!_db.SMSSettings.Any(s => s.IsActive == true))
            {
                await _db.SMSSettings.AddAsync(new SMSSettings
                {
                    UserName = "AC75187ff3d099e419800fa56d4a26a837",
                    Password = EncryptProvider.AESEncrypt("db0de97ff5756d75fa3baf7696098b49", _passwordAESEncryptionOptions.Key),
                    CreatorUserId = UserConstants.SystemUserId,
                    SourcePhoneNumber = "+18722053910",
                    IsActive = true
                });
                await _db.SaveChangesAsync();
            }
        }
    }
}
