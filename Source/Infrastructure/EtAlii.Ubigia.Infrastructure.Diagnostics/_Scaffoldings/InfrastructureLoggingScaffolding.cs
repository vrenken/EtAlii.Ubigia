// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public InfrastructureLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IInfrastructure), typeof(LoggingInfrastructureDecorator));

                // Logical.
                container.RegisterDecorator(typeof(IEntryPreparer), typeof(LoggingEntryPreparerDecorator));

                container.RegisterDecorator(typeof(IStorageRepository), typeof(LoggingStorageRepositoryDecorator));
            }
        }
    }
}
