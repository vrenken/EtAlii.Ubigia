namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class DiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<IDiagnosticsConfiguration>(() => _diagnostics);

            container.Register<IProfilerFactory>(() => _diagnostics.CreateProfilerFactory());
            container.Register<IProfiler>(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            container.Register<ILogFactory>(() => _diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));
        }
    }
}
