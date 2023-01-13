// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public class FabricContextDiagnosticsExtension : IExtension
{
    private readonly IConfigurationRoot _configurationRoot;

    public FabricContextDiagnosticsExtension(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        var options = _configurationRoot
            .GetSection("Infrastructure:Fabric:Diagnostics")
            .Get<DiagnosticsOptions>();

        var scaffoldings = new IScaffolding[]
        {
            new FabricContextLoggingScaffolding(options),
        };

        foreach (var scaffolding in scaffoldings)
        {
            scaffolding.Register(container);
        }
    }
}
