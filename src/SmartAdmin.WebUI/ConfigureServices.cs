using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using SmartAdmin.WebUI.Filters;
using SmartAdmin.WebUI.Models;

namespace SmartAdmin.WebUI;
public static class ConfigureServices
{
    public static IServiceCollection AddRazorPageServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmartSettings>(configuration.GetSection(SmartSettings.SectionName));
        services.AddSingleton(s => s.GetRequiredService<IOptions<SmartSettings>>().Value);
        services.AddHealthChecks();
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllers();
        services.AddSignalR();
        return services;
    }
}
