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
        Task Send(string to, string subject, string content);
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

        public async Task Send(string to, string subject, string content)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("info@esme.community", "esme"));
            msg.AddTo(new EmailAddress(to));
            //msg.SetTemplateId("...");
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Text, content);
            msg.AddContent(MimeType.Html, content);

            var sendgrid = new SendGridClient(_options.SendGridKey);
            await sendgrid.SendEmailAsync(msg);

            _logger.LogInformation($"Sent mail to '{to}' about '{subject}' saying: '{content}'");
        }
    }

    public class LoggingMailingService : IMailingService
    {
        private readonly ILogger<IMailingService> _logger;

        public LoggingMailingService(ILogger<IMailingService> logger)
        {
            _logger = logger;
        }

        public Task Send(string to, string subject, string content)
        {
            _logger.LogInformation($"Sent mail to '{to}' about '{subject}' saying: '{content}'");
            return Task.CompletedTask;
        }
    }
}
