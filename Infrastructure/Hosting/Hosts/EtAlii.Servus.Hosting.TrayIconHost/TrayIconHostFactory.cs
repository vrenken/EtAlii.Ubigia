namespace EtAlii.Servus.Hosting.TrayIconHost
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Hosting.Owin;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;

    public class TrayIconHostFactory : OwinSelfHostedHostFactory<TrayIconHost>
    {
        public TrayIconHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }

        public override IHost Create(
            IInfrastructureFactory infrastructureFactory, 
            IStorageFactory storageFactory,
            IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TrayIconHostScaffolding(),
            };
            return base.Create(infrastructureFactory, storageFactory, diagnostics, scaffoldings);
        }
    }
}
