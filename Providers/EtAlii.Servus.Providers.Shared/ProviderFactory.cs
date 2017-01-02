namespace EtAlii.Servus.Provisioning
{
    using System;
    using SimpleInjector;

    public class ProviderFactory<TProvider> : IProviderFactory
        where TProvider : class, IProvider
    {
        public ProviderFactory()
        {
        }

        //public virtual IProvider Create(ProviderComponent[] components, IDiagnosticsConfiguration diagnostics)
        //{
        //    return Create(components, diagnostics, new IScaffolding[] { });
        //}


        //public IProvider Create(ProviderComponent[] components, IDiagnosticsConfiguration diagnostics,
        //    IScaffolding[] additionalScaffoldings)
        //{
        //}

        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            var scaffoldings = new IScaffolding[]
            {
                new ProvisioningScaffolding<TProvider>(configuration), 

                //new ProvisioningDiagnosticsScaffolding(diagnostics), 
                //new ProvisioningProfilingScaffolding(diagnostics), 
                //new ProvisioningLoggingScaffolding(diagnostics), 
                //new ProvisioningDebuggingScaffolding(diagnostics), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IProvider>();
        }
    }
}
