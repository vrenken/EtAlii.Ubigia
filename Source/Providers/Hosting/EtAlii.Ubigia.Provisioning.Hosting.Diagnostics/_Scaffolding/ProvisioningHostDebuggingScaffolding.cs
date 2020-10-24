﻿namespace EtAlii.Ubigia.Provisioning.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ProvisioningHostDebuggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningHostDebuggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableDebugging) // diagnostics is enabled
            {
                // Invoke all DI container registrations involved in debugging the provisioning host.
            }
        }
    }
}