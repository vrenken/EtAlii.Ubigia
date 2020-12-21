namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionScaffolding : IScaffolding
    {
        private readonly IDataConnectionConfiguration _configuration;

        public DataConnectionScaffolding(IDataConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register<IDataConnection, DataConnection>();
        }
    }
}
