namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class UserPortalScaffolding : IScaffolding
    {
        private readonly IInfrastructureConfiguration _infrastructureConfiguration;

        public UserPortalScaffolding(IInfrastructureConfiguration infrastructureConfiguration)
        {
            _infrastructureConfiguration = infrastructureConfiguration;
        }

        public void Register(Container container)
        {
            //container.Register<IUserPortalComponent, UserPortalComponent>(Lifestyle.Transient);

            //container.Register<ISystemConnection>(() =>
            //{
            //    var infrastructure = _infrastructureConfiguration.GetInfrastructure(container);

            //    var configuration = new SystemConnectionConfiguration()
            //        .Use(infrastructure);
            //    var connection = new SystemConnectionFactory().Create(configuration);
            //    return connection;
            //}, Lifestyle.Singleton);

            RegisterForMicrosoftGraph(container);
            RegisterForGoogle(container);
        }
    }
}