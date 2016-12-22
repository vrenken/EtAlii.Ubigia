namespace EtAlii.Servus.Infrastructure.Transport
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class SystemConnectionScaffolding : xTechnology.MicroContainer.IScaffolding
    {
        private readonly ISystemConnectionConfiguration _configuration;

        public SystemConnectionScaffolding(ISystemConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ISystemConnectionConfiguration>(() => _configuration);
            container.Register<ISystemConnection, SystemConnection>();
        }
    }
}
