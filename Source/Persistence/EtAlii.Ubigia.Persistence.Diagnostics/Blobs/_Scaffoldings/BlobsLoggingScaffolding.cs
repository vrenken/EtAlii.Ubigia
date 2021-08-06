// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    public class BlobsLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;
        private readonly ILogger _logger = Log.ForContext<BlobsLoggingScaffolding>();

        public BlobsLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectLogging) // logging is enabled
            {
                _logger.Verbose("Injecting blob logging decorators");

                container.RegisterDecorator(typeof(IBlobStorer), typeof(LoggingBlobStorer));
                container.RegisterDecorator(typeof(IBlobRetriever), typeof(LoggingBlobRetriever));
                container.RegisterDecorator(typeof(IBlobPartStorer), typeof(LoggingBlobPartStorer));
                container.RegisterDecorator(typeof(IBlobPartRetriever), typeof(LoggingBlobPartRetriever));
            }
        }
    }
}
