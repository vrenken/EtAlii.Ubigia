namespace EtAlii.Ubigia.Infrastructure
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsInfrastructureExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new DiagnosticsScaffolding(_diagnostics),
                new InfrastructureDebuggingScaffolding(_diagnostics),
                new InfrastructureLoggingScaffolding(_diagnostics),
                new InfrastructureProfilingScaffolding(_diagnostics),

                //new WebApiProfilingScaffolding(diagnostics),
                //new WebApiLoggingScaffolding(diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}