namespace EtAlii.Servus.Storage
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsStorageExtension : IStorageExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsStorageExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Storage"),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Storage"));

            var scaffoldings = new IScaffolding[]
            {
                new DiagnosticsScaffolding(diagnostics),
                new BlobsLoggingScaffolding(diagnostics),
                new ComponentsProfilingScaffolding(diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}