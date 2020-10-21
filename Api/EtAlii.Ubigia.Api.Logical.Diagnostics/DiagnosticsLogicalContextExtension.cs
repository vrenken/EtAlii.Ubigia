namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsLogicalContextExtension : ILogicalContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsLogicalContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);
        }
    }
}