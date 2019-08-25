using Microsoft.Extensions.Configuration;

namespace esme.Admin.Server
{
    public static class ConfigurationExtensions
    {
        public static bool UsesLocalhost(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection").Contains("localhost");
        }
    }
}
