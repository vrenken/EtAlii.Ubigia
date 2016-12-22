namespace EtAlii.Servus.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Servus.Infrastructure.Functional;
    using SimpleInjector;

    public class UserPortalInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IInfrastructureConfiguration _infrastructureConfiguration;

        internal UserPortalInfrastructureExtension(IInfrastructureConfiguration infrastructureConfiguration)
        {
            _infrastructureConfiguration = infrastructureConfiguration;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new UserPortalScaffolding(_infrastructureConfiguration), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}