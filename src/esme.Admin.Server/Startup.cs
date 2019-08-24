using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Mime;
using esme.Admin.Shared.Services;
using esme.Admin.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using esme.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using GridMvc;
using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;

namespace esme.Admin.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => _configuration.Bind("AzureAd", options));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 8;
                o.Password.RequiredUniqueChars = 6;

                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                o.Lockout.MaxFailedAccessAttempts = 5;

                o.User.RequireUniqueEmail = true;
            });

            services.AddMvc(options =>
            {
                if (!_environment.OnLocalhost())
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }
            });

            //services.AddSingleton<CircuitHandler, LoggingCircuitHandler>();
            services.AddServerSideBlazor();

            services.AddGridMvc();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                });
            });

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ISampleDataService, SampleDataService>();
            services.AddScoped<IGridService<UserViewModel>, UsersGridService>();
            services.AddScoped<IGridService<CircleViewModel>, CirclesGridService>();
            services.AddScoped<IGridService<InvitationViewModel>, InvitationsGridService>();
            services.AddScoped<IInvitationService, AdminInvitationService>();

            // shared services
            services.AddScoped<InvitationService>();
            if (_environment.OnLocalhost())
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
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            if (!env.OnLocalhost())
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/Index");
            });
        }
    }
}
