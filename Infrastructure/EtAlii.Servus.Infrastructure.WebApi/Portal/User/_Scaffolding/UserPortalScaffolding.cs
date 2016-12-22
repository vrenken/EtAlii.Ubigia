namespace EtAlii.Servus.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using SimpleInjector;

    internal partial class UserPortalScaffolding : IScaffolding
    {
        private readonly IInfrastructureConfiguration _infrastructureConfiguration;

        public UserPortalScaffolding(IInfrastructureConfiguration infrastructureConfiguration)
        {
            _infrastructureConfiguration = infrastructureConfiguration;
        }

        public void Register(Container container)
        {
            container.Register<IUserPortalComponent, UserPortalComponent>(Lifestyle.Transient);

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