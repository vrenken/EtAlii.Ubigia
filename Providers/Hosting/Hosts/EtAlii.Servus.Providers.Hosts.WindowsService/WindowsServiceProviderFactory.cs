namespace EtAlii.Servus.Provisioning.WindowsServiceProvider
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Provisioning;

    public class WindowsServiceProviderFactory : ProviderFactoryBase<WindowsServiceProvider>
    {
        public WindowsServiceProviderFactory(IProviderConfiguration configuration) : base(configuration)
        {
        }

        public override IProvider Create(ProviderComponent[] components, IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new ProviderComponentsScaffolding(), 
            };

            return base.Create(components, diagnostics, scaffoldings);
        }
    }
}
