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
            container.Register(() => _diagnostics);
        }
    }
}