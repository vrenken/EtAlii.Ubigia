// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LoggingFabricContextExtension : IFabricContextExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal LoggingFabricContextExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new();
            configurationRoot.Bind("Api:Fabric:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            if (_configuration.InjectLogging)
            {
                container.RegisterDecorator(typeof(IEntryContext), typeof(LoggingEntryContext));
            }
        }
    }
}
