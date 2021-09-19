using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Behaviours;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Customers.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Workflow.Approval.Steps;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CleanArchitecture.Razor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddLazyCache();
            return services;
        }
        public static IServiceCollection AddWorkflowSteps(this IServiceCollection services, Func<Type, bool> predicate, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetCallingAssembly() };
            }
            assemblies
               .SelectMany(x => x.GetExportedTypes()
               .Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested))
               .Where(predicate)
               .ToList()
               .ForEach(type =>
                        services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Transient))
               );
            return services;
        }
    }
}
