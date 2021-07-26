// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LoggingSchemaProcessorExtension : ISchemaProcessorExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public LoggingSchemaProcessorExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Api:Functional:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            if (_configuration.InjectLogging)
            {
                container.RegisterDecorator(typeof(ISchemaProcessor), typeof(LoggingSchemaProcessor));
            }
        }
    }
}
