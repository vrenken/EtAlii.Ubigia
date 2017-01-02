namespace EtAlii.Servus.Provisioning.TrayIconProvider
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Provisioning;

    public class TrayIconProviderFactory : ProviderFactoryBase<TrayIconProvider>
    {
        public TrayIconProviderFactory(IProviderConfiguration configuration) 
            : base(configuration)
        {
        }

        public override IProvider Create(ProviderComponent[] components, IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TrayIconProviderScaffolding(),
                new ProviderComponentsScaffolding(), 
            };

            return base.Create(components, diagnostics, scaffoldings);
        }
    }
}
