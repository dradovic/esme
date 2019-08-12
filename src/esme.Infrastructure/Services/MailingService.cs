using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace esme.Infrastructure.Services
{
    public class MailingService
    {
        private readonly ILogger<MailingService> _logger;

        public MailingService(ILogger<MailingService> logger)
        {
            _logger = logger;
        }

        internal async Task Send(string to, string subject, string content)
        {
            _logger.LogInformation($"Sent mail to '{to}' about '{subject}' saying: '{content}'");
        }
    }
}
