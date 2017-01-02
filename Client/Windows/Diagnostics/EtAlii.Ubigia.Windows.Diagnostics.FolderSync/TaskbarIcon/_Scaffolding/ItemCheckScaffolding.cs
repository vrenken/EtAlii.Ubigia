namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using SimpleInjector;

    public class ItemCheckScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IItemChecker, ItemChecker>(Lifestyle.Transient);
            container.Register<IItemUpdater, ItemUpdater>(Lifestyle.Transient);

            container.Register<ItemCreatedHandler>(Lifestyle.Transient);
            container.Register<ItemDestroyedHandler>(Lifestyle.Transient);
            container.Register<ItemChangedHandler>(Lifestyle.Transient);
            container.Register<ItemRenameHandler>(Lifestyle.Transient);
            container.Register<DirectoryHelper>(Lifestyle.Transient);
            container.Register<StringEscaper>(Lifestyle.Transient);
            container.Register<FolderUpdater>(Lifestyle.Transient);
            container.Register<LocalPathSplitter>(Lifestyle.Transient);
        }
    }
}
