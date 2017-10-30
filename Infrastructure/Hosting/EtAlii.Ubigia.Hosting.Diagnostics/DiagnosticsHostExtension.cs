namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsHostExtension : IHostExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsHostExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            //var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
            //    () => new DisabledLogFactory(),
            //    () => new DisabledProfilerFactory(),
            //    (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Hosting"),
            //    (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Hosting"));

            //container.Register<IDiagnosticsConfiguration>(() => diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new HostDiagnosticsScaffolding(_diagnostics),
                new HostDebuggingScaffolding(_diagnostics),
                new HostLoggingScaffolding(_diagnostics),
                new HostProfilingScaffolding(_diagnostics),

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