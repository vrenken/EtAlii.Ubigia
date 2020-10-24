namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.MicroContainer;

    internal class FabricContextScaffolding : IScaffolding
    {
        private readonly IStorage _storage;

        public FabricContextScaffolding(IStorage storage)
        {
            _storage = storage;
        }

        public void Register(Container container)
        {
            container.Register<IFabricContext, FabricContext>();
            container.Register(() => _storage);
        }
    }
}