// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public DataConnectionLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator<IDataConnection, LoggingDataConnection>();
            }
        }
    }
}
