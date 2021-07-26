// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class BlobsLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public BlobsLoggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectLogging) // logging is enabled
            {
                container.RegisterDecorator(typeof(IBlobStorer), typeof(LoggingBlobStorer));
                container.RegisterDecorator(typeof(IBlobRetriever), typeof(LoggingBlobRetriever));
                container.RegisterDecorator(typeof(IBlobPartStorer), typeof(LoggingBlobPartStorer));
                container.RegisterDecorator(typeof(IBlobPartRetriever), typeof(LoggingBlobPartRetriever));
            }
        }
    }
}
