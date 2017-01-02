namespace EtAlii.Servus.Infrastructure.Tests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Hosting;
    using EtAlii.Servus.Infrastructure.WebApi;
    using EtAlii.Servus.Storage;

    public class TestInfrastructureFactory : InfrastructureFactoryBase<TestInfrastructure>
    {
        public TestInfrastructureFactory(IInfrastructureConfiguration configuration) 
            : base(configuration)
        {
        }

        public override IInfrastructure Create(IStorage storage, IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SignalRScaffolding(), 
                new WebApiScaffolding<TestAuthenticationIdentityProvider>(),
                new WebApiProfilingScaffolding(diagnostics),
                new WebApiLoggingScaffolding(diagnostics), 
            };

            return Create(storage, diagnostics, scaffoldings);
        }
    }
}
