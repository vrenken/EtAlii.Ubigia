namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsGraphPathTraverserExtension : IGraphPathTraverserExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsGraphPathTraverserExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
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