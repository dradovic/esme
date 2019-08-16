using Microsoft.AspNetCore.Hosting;

namespace esme.Admin.Server
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool OnLocalhost(this IWebHostEnvironment environment)
        {
            return environment.WebRootPath.Contains("\\Users\\");
        }
    }
}
