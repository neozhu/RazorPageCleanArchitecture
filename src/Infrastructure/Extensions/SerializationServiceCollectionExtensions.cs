using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces.Serialization;
using CleanArchitecture.Razor.Infrastructure.Services.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Razor.Infrastructure.Extensions;
public static class SerializationServiceCollectionExtensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
        => services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
}

