namespace EtAlii.Servus.Storage.InMemory
{
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryFactoryScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IInMemoryItems, InMemoryItems>();
            container.Register<IInMemoryItemsHelper, InMemoryItemsHelper>();
        }
    }
}
