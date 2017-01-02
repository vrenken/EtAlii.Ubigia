namespace EtAlii.Servus.Hosting.Owin
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;

    public class OwinSelfHostedHostFactory<THost> : HostFactoryBase<THost>
        where THost : OwinSelfHostedHost
    {
        public OwinSelfHostedHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }

        public override IHost Create(IInfrastructureFactory infrastructureFactory, IStorageFactory storageFactory, IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                
            };

            return base.Create(infrastructureFactory, storageFactory, diagnostics, scaffoldings);
        }
    }
}