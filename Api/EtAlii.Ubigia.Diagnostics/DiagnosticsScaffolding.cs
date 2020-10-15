namespace EtAlii.Ubigia.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Diagnostics.Serilog;
    using EtAlii.xTechnology.MicroContainer;

    internal class DiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScaffolding()
        {
            _diagnostics = DiagnosticsConfiguration.Default;
        }

        public void Register(Container container)
        {
            DiagnosticsConfiguration.Default.UseSerilog();

            container.Register(() => _diagnostics);
        }
    }
}
