namespace EtAlii.Ubigia.Infrastructure.Functional
{
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
            container.Register(() => _configuration);
            container.Register(() => _configuration.Logical);
            container.Register(() => _configuration.SystemConnectionCreationProxy);
        }
    }
}