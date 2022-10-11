// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class FunctionalLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public FunctionalLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator<IFunctionalContext, LoggingFunctionalContextDecorator>();
                container.RegisterDecorator<IStorageRepository, LoggingStorageRepositoryDecorator>();
            }
        }
    }
}
