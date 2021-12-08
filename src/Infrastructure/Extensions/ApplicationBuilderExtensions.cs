using System.DirectoryServices.AccountManagement;
using CleanArchitecture.Razor.Application.Hubs;
using CleanArchitecture.Razor.Application.Hubs.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Context;

namespace CleanArchitecture.Razor.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) => {
                //This didn't work when tested
                diagnosticContext.Set("UserName", httpContext.User.GetDisplayName());
            };
        });
        app.Use(async (httpContext, next) =>
        {
            //This is the correct implementation 
            LogContext.PushProperty("UserName", httpContext.User.GetDisplayName()); //Push user in LogContext;
            await next.Invoke();
        });
        app.UseMiddlewares();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
            RequestPath = new PathString("/Files")
        });

        app.UseRequestLocalization();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
       
        //app.UseWorkflow();
       

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
            endpoints.MapHub<SignalRHub>(SignalR.HubUrl);
        });
        return app;
    }
}
