namespace EtAlii.Servus.Provisioning.ConsoleProvider
{
    using EtAlii.Servus.Api;

    public class ConsoleProviderFactory : ProviderFactoryBase<ConsoleProvider>
    {
        public ConsoleProviderFactory(IProviderConfiguration configuration) : base(configuration)
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
