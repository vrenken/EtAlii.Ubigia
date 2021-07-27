// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LoggingTraversalContextExtension : ITraversalContextExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal LoggingTraversalContextExtension(IConfiguration configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Api:Logical:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            if (_configuration.InjectLogging)
            {
                //container.Register(() => _diagnostics);
            }
        }
    }
}
