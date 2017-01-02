namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using SimpleInjector;

    public class WebApiInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IApplicationManager _applicationManager;

        internal WebApiInfrastructureExtension(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new WebApiApiScaffolding<DefaultAuthenticationIdentityProvider>(_applicationManager),
                new WebApiUserApiScaffolding(),
                new WebApiAdminApiScaffolding(),

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