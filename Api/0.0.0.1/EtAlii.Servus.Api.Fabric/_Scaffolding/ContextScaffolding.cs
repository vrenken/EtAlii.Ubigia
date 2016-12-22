namespace EtAlii.Servus.Api.Fabric
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly IDataConnection _connection;

        public ContextScaffolding(IDataConnection connection)
        {
            _connection = connection;
        }

        public void Register(Container container)
        {
            container.Register<IFabricContext, FabricContext>();
            container.Register<IFabricContextConfiguration, FabricContextConfiguration>();

            container.Register<IDataConnection>(() => _connection);
        }
    }
}
