namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.WebApi;
    using System.Web.Http;
    using EtAlii.Servus.Storage;

    public sealed class WebApiInfrastructureFactory : InfrastructureFactoryBase<WebApiInfrastructure>
    {
        public WebApiInfrastructureFactory(IInfrastructureConfiguration configuration)
            : base(configuration)
        {
        }

        public override IInfrastructure Create(IStorage storage, IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new WebApiInfrastructureScaffolding<DefaultAuthenticationIdentityProvider>(),
                new WebApiInfrastructureProfilingScaffolding(diagnostics),
                new WebApiInfrastructureLoggingScaffolding(diagnostics), 
            };

            return Create(storage, diagnostics, scaffoldings);
        }
    }
}