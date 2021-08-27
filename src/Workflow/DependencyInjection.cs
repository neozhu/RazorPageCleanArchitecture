using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using CleanArchitecture.Razor.Application.Settings;
using CleanArchitecture.Razor.Workflow.Approval.Data;
using CleanArchitecture.Razor.Workflow.Approval;

namespace CleanArchitecture.Razor.Workflow
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkflow(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddWorkflow();
            }
            else
            {
                services.AddWorkflow(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),true,true));
            }
            
            return services;
        }
        public static IApplicationBuilder UseWorkflow(this IApplicationBuilder app)
        {
            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            host.RegisterWorkflow<DocmentApprovalWorkflow,ApprovalData>();
            host.Start();

            var appLifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            appLifetime.ApplicationStopping.Register(() =>
            {
                host.Stop();
            });

            return app;
        }
    }
}
