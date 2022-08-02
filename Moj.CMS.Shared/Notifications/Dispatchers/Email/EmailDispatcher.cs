using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Settings;
using NETCore.Encrypt;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Moj.CMS.Shared.Notifications.Dispatchers.Email
{
    public class EmailDispatcher : IEmailDispatcher
    {
        private readonly PasswordAESEncryptionOptions _passwordAESEncryptionOptions;
        private readonly ISettingsQueries _settingsQueries;
        private EmailSettingsDto _emailSettingsDto;

        public EmailDispatcher(IOptions<PasswordAESEncryptionOptions> passwordEncryptionOptions, ISettingsQueries settingsQueries)
        {
            _passwordAESEncryptionOptions = passwordEncryptionOptions.Value;
            _settingsQueries = settingsQueries;
        }

        public async Task DispatchAsync(EmailInput emailInput)
        {
            _emailSettingsDto = await _settingsQueries.GetEmailSettingsAsync();

            var emailMessage = CreateEmailMessage(emailInput);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailInput emailInput)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettingsDto.DisplayName, _emailSettingsDto.Email));
            emailMessage.To.AddRange(emailInput.To.Select(p => new MailboxAddress(p)).ToList());
            emailMessage.Subject = emailInput.Subject;
            emailMessage.Cc.AddRange(emailInput.CC.Select(p => new MailboxAddress(p)).ToList());
            var builder = new BodyBuilder()
            {
                HtmlBody = emailInput.MessageBody
            };

            #region Attachments //ToDo

            //if (emailInput.AttachmentPath != null && emailInput.AttachmentPath.Any())
            //{
            //    foreach (var path in emailInput.AttachmentPath)
            //    {
            //        builder.Attachments.Add(path);
            //    }
            //}

            #endregion

            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            if (_emailSettingsDto != null)
            {
                using var client = new SmtpClient();

                try
                {
                    Guard.AssertArgumentNotNullOrEmptyOrWhitespace(_emailSettingsDto.EmailHost,
                        nameof(_emailSettingsDto.EmailHost));
                    Guard.AssertArgumentNotNull(_emailSettingsDto.Port, nameof(_emailSettingsDto.Port));
                    Guard.AssertArgumentNotNull(_emailSettingsDto.UseSsl, nameof(_emailSettingsDto.UseSsl));
                    Guard.AssertArgumentNotNullOrEmptyOrWhitespace(_emailSettingsDto.Email, nameof(_emailSettingsDto.Email));
                    Guard.AssertArgumentNotNullOrEmptyOrWhitespace(_emailSettingsDto.Password, nameof(_emailSettingsDto.Password));

                    await client.ConnectAsync(_emailSettingsDto.EmailHost, _emailSettingsDto.Port, _emailSettingsDto.UseSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    var password = EncryptProvider.AESDecrypt(_emailSettingsDto.Password, _passwordAESEncryptionOptions.Key);
                    await client.AuthenticateAsync(_emailSettingsDto.Email, password);

                    await client.SendAsync(emailMessage);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
