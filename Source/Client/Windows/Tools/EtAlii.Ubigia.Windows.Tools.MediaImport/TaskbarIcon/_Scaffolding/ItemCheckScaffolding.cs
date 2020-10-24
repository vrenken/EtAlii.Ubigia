namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.xTechnology.MicroContainer;

    public class ItemCheckScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IItemChecker, ItemChecker>();
            container.Register<IItemUpdater, ItemUpdater>();

            container.Register<IItemCreatedHandler, ItemCreatedHandler>();
            container.Register<IItemDestroyedHandler, ItemDestroyedHandler>();
            container.Register<IItemChangedHandler, ItemChangedHandler>();
            container.Register<IItemRenameHandler, ItemRenameHandler>();
            container.Register<IDirectoryHelper, DirectoryHelper>();
            container.Register<IStringEscaper, StringEscaper>();
            container.Register<IFolderUpdater, FolderUpdater>();
            container.Register<ILocalPathSplitter, LocalPathSplitter>();
        }
    }
}
