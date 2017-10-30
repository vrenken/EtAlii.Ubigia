namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    public class HostScaffolding<THost> : IScaffolding
        where THost : class, IHost
    {
        private readonly IHostConfiguration _configuration;

        public HostScaffolding(IHostConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IHost, THost>();
            container.Register<IHostConfiguration>(() => _configuration);
        }
    }
}
