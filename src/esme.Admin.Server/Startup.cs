using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using esme.Admin.Shared.Services;
using esme.Admin.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using esme.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using GridMvc;
using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;

namespace esme.Admin.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => _configuration.Bind("AzureAd", options));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(o =>
            {
                o.User.RequireUniqueEmail = true; // note: important when sending invitations from Admin
            });

            services.AddMvc(options =>
            {
                if (!_configuration.UsesLocalhost())
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddGridMvc();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ISampleDataService, SampleDataService>();
            services.AddScoped<IGridService<UserViewModel>, UsersGridService>();
            services.AddScoped<IGridService<CircleViewModel>, CirclesGridService>();
            services.AddScoped<IGridService<InvitationViewModel>, InvitationsGridService>();
            services.AddScoped<IInvitationService, AdminInvitationService>();

            // shared services
            services.AddScoped<InvitationService>();
            if (_configuration.UsesLocalhost())
            {
                services.AddScoped<IMailingService, LoggingMailingService>();
            }
            else
            {
                services.AddScoped<IMailingService, MailingService>();
            }

            services.AddOptions();
            services.Configure<MailingOptions>(_configuration.GetSection("SendGrid"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            if (!_configuration.UsesLocalhost())
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub<App.App>(selector: "app");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
