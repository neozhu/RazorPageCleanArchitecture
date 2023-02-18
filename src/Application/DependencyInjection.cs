using CleanArchitecture.Razor.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CleanArchitecture.Razor.Application.Invoices.PaddleOCR;
using System.Net.Http.Headers;
using Hangfire;
using Polly;
using Hangfire.MemoryStorage;
using MediatR.Pipeline;

namespace CleanArchitecture.Razor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(RequestExceptionProcessorBehavior<,>));
                config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                config.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                config.AddOpenBehavior(typeof(CachingBehaviour<,>));
                config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));
                config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                

            });

            services.AddLazyCache();
            services.AddTransient<IInvoicesOcrJob, InvoicesOcrJob>();
            services.AddHangfire(options =>
            {
                options.UseMemoryStorage();

            });

            services.AddHangfireServer(options => {
                options.WorkerCount = 1;
                });


            services.AddHttpClient("ocr", c =>
            {
                c.BaseAddress = new Uri("https://paddleocr.i247365.net/predict/ocr_system");
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000))); ;
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
