namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class EntryScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IEntrySet, EntrySet>();

            container.Register<IEntryUpdater, EntryUpdater>();
            container.Register<IEntryGetter, EntryGetter>();
            container.Register<IEntryStorer, EntryStorer>();
        }
    }
}