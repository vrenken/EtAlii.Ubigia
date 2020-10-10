namespace EtAlii.Ubigia.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class DiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScaffolding()
        {
            _diagnostics = UbigiaDiagnostics.DefaultConfiguration;
        }

        public void Register(Container container)
        {
            // var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
            //     () => new DisabledLogFactory(),
            //     () => new DisabledProfilerFactory(),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"),
            //     (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"));

            container.Register(() => _diagnostics);
        }
    }
}
