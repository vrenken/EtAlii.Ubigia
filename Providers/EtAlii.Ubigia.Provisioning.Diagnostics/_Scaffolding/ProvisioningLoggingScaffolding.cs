namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ProvisioningLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register(() => _diagnostics.CreateLogFactory());
            container.Register(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (_diagnostics.EnableLogging) // logging is enabled.
            {
            }
        }
    }
}