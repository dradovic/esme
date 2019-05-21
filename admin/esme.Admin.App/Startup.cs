using Microsoft.Extensions.DependencyInjection;
using esme.Admin.App.Services;
using Microsoft.AspNetCore.Components.Builder;

namespace esme.Admin.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Since Blazor is running on the server, we can use an application service
            // to read the forecast data.
            services.AddSingleton<WeatherForecastService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
