namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class AdminPortalScaffolding : IScaffolding
    {
        private readonly IInfrastructureConfiguration _infrastructureConfiguration;

        public AdminPortalScaffolding(IInfrastructureConfiguration infrastructureConfiguration)
        {
            _infrastructureConfiguration = infrastructureConfiguration;
        }

        public void Register(Container container)
        {
            //container.Register<IAdminPortalComponent, AdminPortalComponent>(Lifestyle.Transient);

            // TODO: This should be a system connection provider and not a system connection.
            container.Register<ISystemConnection>(() =>
            {
                var infrastructure = _infrastructureConfiguration.GetInfrastructure(container);

                var configuration = new SystemConnectionConfiguration()
                    .Use(SystemTransportProvider.Create(infrastructure))
                    .Use(infrastructure);
                var connection = new SystemConnectionFactory().Create(configuration);
                return connection;
            });

            RegisterForMicrosoftGraph(container);
            RegisterForGoogle(container);
        }
    }
}