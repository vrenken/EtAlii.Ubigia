namespace EtAlii.Servus.Provisioning
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsProviderExtension : IProviderExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsProviderExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Provisioning"),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Provisioning"));

            var scaffoldings = new IScaffolding[]
            {
                new ProvisioningDiagnosticsScaffolding(diagnostics),
                new ProvisioningDebuggingScaffolding(diagnostics),
                new ProvisioningLoggingScaffolding(diagnostics),
                new ProvisioningProfilingScaffolding(diagnostics),

                //new WebApiProfilingScaffolding(diagnostics),
                //new WebApiLoggingScaffolding(diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}