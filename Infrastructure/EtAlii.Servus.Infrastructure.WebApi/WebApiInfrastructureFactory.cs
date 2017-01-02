namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    public sealed class WebApiInfrastructureFactory : InfrastructureFactoryBase<WebApiInfrastructure>
    {
        private readonly IApplicationManager _applicationManager;

        public WebApiInfrastructureFactory(IInfrastructureConfiguration configuration)
            : base(configuration)
        {
        }

        public WebApiInfrastructureFactory(IInfrastructureConfiguration configuration, IApplicationManager applicationManager)
            : base(configuration)
        {
            _applicationManager = applicationManager;
        }

        public override IInfrastructure Create(IStorage storage, IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SignalRScaffolding(), 
                new WebApiScaffolding<DefaultAuthenticationIdentityProvider>(_applicationManager),
                new WebApiProfilingScaffolding(diagnostics),
                new WebApiLoggingScaffolding(diagnostics), 
            };

            return Create(storage, diagnostics, scaffoldings);
        }
    }
}