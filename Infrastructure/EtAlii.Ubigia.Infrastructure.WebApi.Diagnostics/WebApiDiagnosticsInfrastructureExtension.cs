namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiDiagnosticsInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal WebApiDiagnosticsInfrastructureExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new WebApiProfilingScaffolding(_diagnostics),
                new WebApiLoggingScaffolding(_diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}