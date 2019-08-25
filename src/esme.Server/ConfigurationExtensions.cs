using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace esme.Server
{
    public static class ConfigurationExtensions
    {
        public static bool UsesLocalhost(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection").Contains("localhost");
        }
    }
}
