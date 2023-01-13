// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#pragma warning disable // This is an external file.

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Reflection;

internal class ConfigureContainerBuilder
{
    public ConfigureContainerBuilder(MethodInfo configureContainerMethod)
    {
        MethodInfo = configureContainerMethod;
    }

    public MethodInfo MethodInfo { get; }

    public Func<Action<object>, Action<object>> ConfigureContainerFilters { get; set; } = f => f;

    public Action<object> Build(object instance) => container => Invoke(instance, container);

    public Type GetContainerType()
    {
        var parameters = MethodInfo.GetParameters();
        if (parameters.Length != 1)
        {
            // REVIEW: This might be a breaking change
            throw new InvalidOperationException($"The {MethodInfo.Name} method must take only one parameter.");
        }
        return parameters[0].ParameterType;
    }

    private void Invoke(object instance, object container)
    {
        var containerBuilder = ConfigureContainerFilters(cb => InvokeCore(instance, cb));
        containerBuilder(container);
    }

    private void InvokeCore(object instance, object container)
    {
        if (MethodInfo == null)
        {
            return;
        }

        var arguments = new[] { container };

        MethodInfo.InvokeWithoutWrappingExceptions(instance, arguments);
    }
}
