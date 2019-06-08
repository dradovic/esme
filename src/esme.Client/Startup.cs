using Blazor.Extensions;
using esme.Client.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace esme.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HubConnectionBuilder>();

            services.AddScoped<AuthenticationState>();
            services.AddScoped<IAuthorizationApi, AuthorizationApi>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
