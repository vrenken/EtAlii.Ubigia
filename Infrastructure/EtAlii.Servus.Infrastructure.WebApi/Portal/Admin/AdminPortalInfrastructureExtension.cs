namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using EtAlii.Servus.Infrastructure.Functional;
    using SimpleInjector;

    public class AdminPortalInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IInfrastructureConfiguration _infrastructureConfiguration;

        internal AdminPortalInfrastructureExtension(IInfrastructureConfiguration infrastructureConfiguration)
        {
            _infrastructureConfiguration = infrastructureConfiguration;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new AdminPortalScaffolding(_infrastructureConfiguration), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}