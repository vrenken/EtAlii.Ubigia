namespace EtAlii.Servus.Infrastructure.Functional
{
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.Transport;
    using SimpleInjector;

    internal class InfrastructureScaffolding : IScaffolding
    {
        private readonly IInfrastructureConfiguration _configuration;

        public InfrastructureScaffolding(IInfrastructureConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IInfrastructureConfiguration>(() => _configuration, Lifestyle.Singleton);
            container.Register<ILogicalContext>(() => _configuration.Logical, Lifestyle.Singleton);
            container.Register<ISystemConnectionCreationProxy>(() => _configuration.SystemConnectionCreationProxy, Lifestyle.Singleton);
        }
    }
}