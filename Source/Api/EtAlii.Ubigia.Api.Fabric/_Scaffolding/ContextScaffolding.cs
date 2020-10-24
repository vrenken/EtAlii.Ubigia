namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
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

            container.Register(() => _connection);
        }
    }
}
