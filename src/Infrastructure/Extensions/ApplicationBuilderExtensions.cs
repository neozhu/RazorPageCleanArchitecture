using CleanArchitecture.Razor.Application.Hubs;
using CleanArchitecture.Razor.Application.Hubs.Constants;
using CleanArchitecture.Razor.Application.Workflow.Approval;
using CleanArchitecture.Razor.Domain.Entities.Worflow;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Context;
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) => {
                //This didn't work when tested
                diagnosticContext.Set("UserName", httpContext.User?.Identity?.Name ?? "Anonymous");
            };
        });
        app.Use(async (httpContext, next) =>
        {
            //This is the correct implementation 
            var userName = httpContext.User?.Identity?.Name ?? "Anonymous"; //Gets user Name from user Identity
            LogContext.PushProperty("UserName", userName); //Push user in LogContext;
            await next.Invoke();
        });
       
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
            RequestPath = new PathString("/Files")
        });

        app.UseRequestLocalization();
        app.UseMiddlewares();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
       
       // app.UseWorkflow();
       

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
            endpoints.MapHub<SignalRHub>(SignalR.HubUrl);
        });

        // enable workflow core
        var host = app.ApplicationServices.GetService<IWorkflowHost>();
        host.RegisterWorkflow<DocmentApprovalWorkflow, ApprovalData>();
        host.Start();

        return app;
    }
}
