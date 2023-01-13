// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public class LoggingSchemaProcessorExtension : IExtension
{
    private readonly IConfigurationRoot _configurationRoot;

    public LoggingSchemaProcessorExtension(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        var options = _configurationRoot
            .GetSection("Api:Functional:Diagnostics")
            .Get<DiagnosticsOptions>();

        if (options?.InjectLogging ?? false)
        {
            var hasProcessor = container.HasRegistration<ISchemaProcessor>();
            if (hasProcessor)
            {
                container.RegisterDecorator<ISchemaProcessor, LoggingSchemaProcessor>();
            }
        }
    }
}
