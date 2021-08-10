// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ManagementConnectionDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public ManagementConnectionDebuggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectDebugging) // debugging is enabled
            {
                // Invoke all DI container registrations involved in debugging the ManagementConnection.
            }
        }
    }
}
