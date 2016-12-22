namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsDataContextExtension : IDataContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsDataContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"));

            container.Register<IDiagnosticsConfiguration>(() => diagnostics);
        }
    }
}