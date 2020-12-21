namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsTraversalScriptContextExtension : ITraversalScriptContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsTraversalScriptContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);
        }
    }
}
