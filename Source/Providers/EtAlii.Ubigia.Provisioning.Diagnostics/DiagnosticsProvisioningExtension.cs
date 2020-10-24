namespace EtAlii.Ubigia.Provisioning.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsProvisioningExtension : IProvisioningExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsProvisioningExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            // var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
            //     () => new DisabledLogFactory(),
            //     () => new DisabledProfilerFactory(),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Provisioning"),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Provisioning"));

            var scaffoldings = new IScaffolding[]
            {
                new ProvisioningDiagnosticsScaffolding(_diagnostics),
                new ProvisioningDebuggingScaffolding(_diagnostics),
                new ProvisioningLoggingScaffolding(_diagnostics),
                new ProvisioningProfilingScaffolding(_diagnostics),

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