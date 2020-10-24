namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class ItemsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IItemStorage, ItemStorage>();
        }
    }
}
