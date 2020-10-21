namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsManagementConnectionExtension : IManagementConnectionExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsManagementConnectionExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionLoggingScaffolding(),
                new ManagementConnectionProfilingScaffolding(),
                new ManagementConnectionDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}