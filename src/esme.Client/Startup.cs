using Blazor.Fluxor;
using esme.Client.Services;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace esme.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluxor(o =>
            {
                o.UseDependencyInjection(typeof(Startup).Assembly);
            });

            //services.AddTransient<HubConnectionBuilder>();

            services.AddAuthorizationCore();
            services.AddScoped<IdentityAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
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
