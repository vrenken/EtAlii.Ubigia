namespace EtAlii.Servus.Hosting.Owin
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;

    public class OwinHostedHostFactory<THost> : HostFactoryBase<THost>
        where THost : OwinHostedHost
    {
        public OwinHostedHostFactory(IHostConfiguration configuration) 
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