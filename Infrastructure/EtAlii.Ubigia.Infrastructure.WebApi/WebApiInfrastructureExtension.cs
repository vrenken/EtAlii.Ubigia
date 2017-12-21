namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiInfrastructureExtension : IInfrastructureExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new WebApiApiScaffolding<DefaultAuthenticationIdentityProvider>(),

                //new WebApiProfilingScaffolding(diagnostics),
                //new WebApiLoggingScaffolding(diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}