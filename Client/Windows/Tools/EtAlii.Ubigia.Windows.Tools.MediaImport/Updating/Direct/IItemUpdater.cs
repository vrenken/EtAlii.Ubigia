namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface IItemUpdater
    {
        FolderSyncConfiguration Configuration { get; set; }

        void Update(ItemCheckAction action);
    }
}
