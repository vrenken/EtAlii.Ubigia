namespace EtAlii.Ubigia.Api.Functional.Diagnostics 
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsGraphTLQueryContextExtension : IGraphTLQueryContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsGraphTLQueryContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);
        }
    }
}