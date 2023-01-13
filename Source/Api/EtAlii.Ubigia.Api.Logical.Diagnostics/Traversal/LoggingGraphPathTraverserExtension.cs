// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public sealed class DiagnosticsGraphPathTraverserExtension : IExtension
{
    private readonly IConfigurationRoot _configurationRoot;

    public DiagnosticsGraphPathTraverserExtension(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        var options = _configurationRoot
            .GetSection("Api:Logical:Diagnostics")
            .Get<DiagnosticsOptions>();

        if (options?.InjectLogging ?? false)
        {
            // Do stuff...
        }
    }
}
