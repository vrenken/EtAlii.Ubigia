// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public StorageConnectionDebuggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectDebugging) // logging is enabled
            {
                // Invoke all DI container registrations involved in debugging the StorageConnection.
            }
        }
    }
}
