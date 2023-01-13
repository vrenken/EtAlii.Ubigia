// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#pragma warning disable // This is an external file.

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

internal class ConfigureBuilder
{
    public ConfigureBuilder(MethodInfo configure)
    {
        MethodInfo = configure;
    }

    public MethodInfo MethodInfo { get; }

    public Action<IApplicationBuilder> Build(object instance) => builder => Invoke(instance, builder);

    private void Invoke(object instance, IApplicationBuilder builder)
    {
        // Create a scope for Configure, this allows creating scoped dependencies
        // without the hassle of manually creating a scope.
        using var scope = builder.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var parameterInfos = MethodInfo.GetParameters();
        var parameters = new object[parameterInfos.Length];
        for (var index = 0; index < parameterInfos.Length; index++)
        {
            var parameterInfo = parameterInfos[index];
            if (parameterInfo.ParameterType == typeof(IApplicationBuilder))
            {
                parameters[index] = builder;
            }
            else
            {
                try
                {
                    parameters[index] = serviceProvider.GetRequiredService(parameterInfo.ParameterType);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Could not resolve a service of type '{parameterInfo.ParameterType.FullName}' for the parameter '{parameterInfo.Name}' of method '{MethodInfo.Name}' on type '{MethodInfo.DeclaringType!.FullName}'.", ex);
                }
            }
        }

        MethodInfo.InvokeWithoutWrappingExceptions(instance, parameters);
    }
}
