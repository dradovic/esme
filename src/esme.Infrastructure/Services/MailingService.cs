using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace esme.Infrastructure.Services
{
    public class MailingOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }

    public interface IMailingService
    {
        Task<string> Send(string to, string subject, string content);
    }

    public class MailingService : IMailingService
    {
        private readonly MailingOptions _options;
        private readonly ILogger<IMailingService> _logger;

        public MailingService(IOptions<MailingOptions> options, ILogger<IMailingService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task<string> Send(string to, string subject, string content)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("info@esme.community", "esme"));
            msg.AddTo(new EmailAddress(to));
            //msg.SetTemplateId("...");
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Text, content);
            msg.AddContent(MimeType.Html, content);

            var sendgrid = new SendGridClient(_options.SendGridKey);

            var response = await sendgrid.SendEmailAsync(msg);
            if (!response.StatusCode.IsSuccess())
            {
                _logger.LogError($"Could not send mail to '{to}' about '{subject}' saying: '{content}'.");
                return await response.Body.ReadAsStringAsync();
            }
            else
            {
                _logger.LogInformation($"Sent mail to '{to}' about '{subject}' saying: '{content}'.");
                return null;
            }
        }
    }

    public class LoggingMailingService : IMailingService
    {
        private readonly ILogger<IMailingService> _logger;

        public LoggingMailingService(ILogger<IMailingService> logger)
        {
            _logger = logger;
        }

        public Task<string> Send(string to, string subject, string content)
        {
            _logger.LogInformation($"Sent mail to '{to}' about '{subject}' saying: '{content}'");
            return Task.FromResult(default(string));
        }
    }
}
