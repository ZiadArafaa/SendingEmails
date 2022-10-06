using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendingEmails.Helpers;

namespace SendingEmails.Services
{
    public class MailServices : IMailServices
    {
        private readonly EmailSettings _emailsettings;
        public MailServices(IOptions<EmailSettings> emailSettings)
        {
            _emailsettings = emailSettings.Value;
        }
        public async Task SendingEmailAsync(string mailto, string sub, string body, IList<IFormFile> attch = null)
        {
            var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_emailsettings.Email),
                Subject = sub,
            };

            email.To.Add(MailboxAddress.Parse(mailto));

            var builder = new BodyBuilder();

            if(attch != null)
            {
                byte[] filebytes;

                foreach(var file in attch)
                {
                    if (file.Length > 0) 
                    {
                        using var ms = new MemoryStream();

                        await file.CopyToAsync(ms);

                        filebytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName, filebytes,ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            email.From.Add(new MailboxAddress(_emailsettings.DisplayName, _emailsettings.Email));

            using var stmp = new SmtpClient();
            stmp.Connect(_emailsettings.Host, _emailsettings.Port,SecureSocketOptions.StartTls);
            stmp.Authenticate(_emailsettings.Email, _emailsettings.Password);
            await stmp.SendAsync(email);
            stmp.Disconnect(true);

        }
    }
}
 