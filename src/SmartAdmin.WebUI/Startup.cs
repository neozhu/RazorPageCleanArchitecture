using CleanArchitecture.Razor.Application;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Infrastructure;
using CleanArchitecture.Razor.Infrastructure.Identity;
using CleanArchitecture.Razor.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.Services;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static IdentityModel.ClaimComparer;

namespace SmartAdmin.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmartSettings>(Configuration.GetSection(SmartSettings.SectionName));

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            // Note: This line is for demonstration purposes only, I would not recommend using this as a shorthand approach for accessing settings
            // While having to type '.Value' everywhere is driving me nuts (>_<), using this method means reloaded appSettings.json from disk will not work
            services.AddSingleton(s => s.GetRequiredService<IOptions<SmartSettings>>().Value);

            services.Configure<CookiePolicyOptions>(options =>
            {
          // This lambda determines whether user consent for non-essential cookies is needed for a given request.
          options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsIdentityFactory>();
            services.AddControllers();
            //services.AddMvc().AddFluentValidation(fv => {
            //    fv.DisableDataAnnotationsValidation = false;
            //    fv.ImplicitlyValidateChildProperties = true;
            //      });
            services
                .AddRazorPages()
                .AddJsonOptions(options =>
                  {
                      options.JsonSerializerOptions.PropertyNamingPolicy = null;
                      
                    })
                .AddRazorRuntimeCompilation()
                .AddRazorPagesOptions(options =>
                {

                    options.Conventions.AddPageRoute("/AspNetCore/Welcome", "");
                });

            services.ConfigureApplicationCookie(options =>
      {
          options.LoginPath = "/Identity/Account/Login";
          options.LogoutPath = "/Identity/Account/Logout";
          options.AccessDeniedPath = "/Identity/Account/AccessDenied";
      });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
