namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ItemsScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IItemsSet, ItemsSet>();
            container.Register<IItemAdder, ItemAdder>();
            container.Register<IItemRemover, ItemRemover>();
            container.Register<IItemGetter, ItemGetter>();
            container.Register<IItemUpdater, ItemUpdater>();
        }
    }
}