// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public sealed class LoggingFabricContextExtension : IExtension
{
    private readonly IConfigurationRoot _configurationRoot;

    public LoggingFabricContextExtension(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        var options = _configurationRoot
            .GetSection("Api:Fabric:Diagnostics")
            .Get<DiagnosticsOptions>();

        if (options?.InjectLogging ?? false)
        {
            container.RegisterDecorator<IEntryContext, LoggingEntryContext>();
        }
    }
}
