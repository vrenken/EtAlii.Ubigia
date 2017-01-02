namespace EtAlii.Servus.Provisioning
{
    using EtAlii.Servus.Api;
    using SimpleInjector;

    public class ProvisioningDiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningDiagnosticsScaffolding(IDiagnosticsConfiguration diagnostics1)
        {
            _diagnostics = diagnostics1;
        }

        public void Register(Container container)
        {
            container.Register<IDiagnosticsConfiguration>(() => _diagnostics, Lifestyle.Singleton);
        }
    }
}