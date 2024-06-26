// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;

namespace CleanArchitecture.Razor.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = (from t in assembly.GetExportedTypes()
                     let mapInterfaces = t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)).ToList()
                     where mapInterfaces.Any()
                     select (t, mapInterfaces)
            )
            .ToList();

        foreach (var (type, mapInterfaces) in types)
        {
            var instance = Activator.CreateInstance(type);

            var classMethodInfo = type.GetMethod("Mapping");
            if (classMethodInfo != null)
                classMethodInfo.Invoke(instance, new object[] { this });
            else
            {
                foreach (var mapInterface in mapInterfaces)
                {
                    var methodInfo = mapInterface.GetMethod("Mapping");
                    methodInfo?.Invoke(instance, new object[] { this });
                }

            }

        }
    }
}
