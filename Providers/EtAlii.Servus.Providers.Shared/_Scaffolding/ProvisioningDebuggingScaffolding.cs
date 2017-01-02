namespace EtAlii.Servus.Provisioning
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class ProvisioningDebuggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningDebuggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableDebugging) // diagnostics is enabled
            {
            }
        }
    }
}