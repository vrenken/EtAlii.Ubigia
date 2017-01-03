namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class InfrastructureScaffolding : IScaffolding
    {
        private readonly IInfrastructureConfiguration _configuration;

        public InfrastructureScaffolding(IInfrastructureConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IInfrastructureConfiguration>(() => _configuration);
            container.Register<ILogicalContext>(() => _configuration.Logical);
            container.Register<ISystemConnectionCreationProxy>(() => _configuration.SystemConnectionCreationProxy);
        }
    }
}