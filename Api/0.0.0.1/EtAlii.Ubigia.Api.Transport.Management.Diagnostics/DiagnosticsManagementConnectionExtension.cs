namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
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
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api.Management"),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api.Management"));

            container.Register<IDiagnosticsConfiguration>(() => diagnostics);

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