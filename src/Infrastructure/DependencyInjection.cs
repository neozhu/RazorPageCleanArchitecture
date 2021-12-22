// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Razor.Application.Settings;
using CleanArchitecture.Razor.Application.Workflow.Approval;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using CleanArchitecture.Razor.Infrastructure.Configurations;
using CleanArchitecture.Razor.Infrastructure.Constants.ClaimTypes;
using CleanArchitecture.Razor.Infrastructure.Constants.Localization;
using CleanArchitecture.Razor.Application.Constants.Permission;
using CleanArchitecture.Razor.Infrastructure.Identity;
using CleanArchitecture.Razor.Infrastructure.Middlewares;
using CleanArchitecture.Razor.Infrastructure.Persistence;
using CleanArchitecture.Razor.Infrastructure.Services;
using CleanArchitecture.Razor.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WorkflowCore.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using CleanArchitecture.Razor.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Localization;
using CleanArchitecture.Razor.Infrastructure.Extensions;

namespace CleanArchitecture.Razor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitecture.RazorDb")
                ); ;
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))

                );
        }
        services.Configure<SmartSettings>(configuration.GetSection(SmartSettings.SectionName));
        services.AddSingleton(s => s.GetRequiredService<IOptions<SmartSettings>>().Value);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddScoped<IDomainEventService, DomainEventService>();


        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IExcelService, ExcelService>();
        services.AddTransient<IUploadService, UploadService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.Configure<AppConfigurationSettings>(configuration.GetSection("AppConfigurationSettings"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IMailService, SMTPMailService>();
        services.AddTransient<IDictionaryService, DictionaryService>();
        services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();
        services.Configure<IdentityOptions>(options =>
        {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
        });
        services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrate", policy => policy.RequireUserName("euro1\\zhuhua"));
            options.AddPolicy("Manager", policy => policy.RequireAssertion(context =>
                       context.User.Identity.Name.ToLower() == "euro1\\zhuhua" ||
                       context.User.Identity.Name.ToLower() == "euro1\\chenjun"));
            options.AddPolicy("ProjectUsers", policy => policy.RequireAssertion(context =>
                       context.User.IsInRole("MOVE-VT-MIG") ||
                       context.User.Identity.Name.ToLower() == "euro1\\zhuhua" ||
                       context.User.Identity.Name.ToLower() == "euro1\\chenjun"));

        });
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsIdentityFactory>();
        // Localization
        services.AddLocalization(options => options.ResourcesPath = LocalizationConstants.ResourcesPath);
        services.AddScoped<LocalizationCookiesMiddleware>();
        services.AddScoped<ExceptionHandlerMiddleware>();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.AddSupportedUICultures(LocalizationConstants.SupportedLanguages.Select(x => x.Code).ToArray());
            options.FallBackToParentUICultures = true;

            options.RequestCultureProviders
                .Remove(options.RequestCultureProviders.FirstOrDefault(x=>x.GetType()== typeof(AcceptLanguageHeaderRequestCultureProvider)));
        });
 
        return services;
    }

    public static IServiceCollection AddWorkflow(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddWorkflow();
        }
        else
        {
            services.AddWorkflow(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), true, true));
        }
        return services;
    }
    public static IApplicationBuilder UseWorkflow(this IApplicationBuilder app)
    {
        var host = app.ApplicationServices.GetService<IWorkflowHost>();
        host.RegisterWorkflow<DocmentApprovalWorkflow, ApprovalData>();
        host.Start();
        return app;
    }
}
