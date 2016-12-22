namespace EtAlii.Servus.Storage
{
    using EtAlii.xTechnology.MicroContainer;

    public class ItemsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IItemStorage, ItemStorage>();
            //container.Register<IItemGetter, ItemGetter>();
        }
    }
}
