namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsLinqQueryContextExtension : ILinqQueryContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsLinqQueryContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var diagnostics = _diagnostics ?? new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"),
                (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Api"));

            container.Register(() => diagnostics);
        }
    }
}