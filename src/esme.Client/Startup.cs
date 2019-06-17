using Blazor.Extensions;
using Blazor.Extensions.Logging;
using Blazor.Fluxor;
using esme.Client.Services;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace esme.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace)
            );

            services.AddFluxor(o =>
            {
                o.UseDependencyInjection(typeof(Startup).Assembly);
            });

            services.AddTransient<HubConnectionBuilder>();

            services.AddScoped<AuthenticationState>();
            services.AddScoped<IAuthorizationApi, AuthorizationApi>();
            services.AddScoped<MessagesApi>();
            services.AddSingleton<IEventAggregator, EventAggregator.Blazor.EventAggregator>();
            services.AddSingleton<ClientHub>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
