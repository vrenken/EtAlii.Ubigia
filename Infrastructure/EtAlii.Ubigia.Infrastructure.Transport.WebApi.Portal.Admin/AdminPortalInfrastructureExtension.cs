namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

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