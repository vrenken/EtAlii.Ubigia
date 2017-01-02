namespace EtAlii.Servus.Provisioning
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class ProvisioningLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<ILogFactory>(() => _diagnostics.CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()), Lifestyle.Transient);
            if (_diagnostics.EnableLogging) // logging is enabled.
            {
            }
        }
    }
}