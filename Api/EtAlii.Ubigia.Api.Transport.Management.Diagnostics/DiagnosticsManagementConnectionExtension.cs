namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsManagementConnectionExtension : IManagementConnectionExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsManagementConnectionExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            // var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
            //     () => new DisabledLogFactory(),
            //     () => new DisabledProfilerFactory(),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api.Management"),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api.Management"));

            container.Register(() => _diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionLoggingScaffolding(),
                new ManagementConnectionProfilingScaffolding(),
                new ManagementConnectionDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}