// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LoggingSchemaProcessorExtension : ISchemaProcessorExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            if (configuration.InjectLogging)
            {
                container.RegisterDecorator(typeof(ISchemaProcessor), typeof(LoggingSchemaProcessor));
            }
        }
    }
}
