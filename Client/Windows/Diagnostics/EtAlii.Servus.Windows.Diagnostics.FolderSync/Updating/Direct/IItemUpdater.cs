namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using System.ComponentModel;

    internal interface IItemUpdater
    {
        FolderSyncConfiguration Configuration { get; set; }

        void Update(ItemCheckAction action);
    }
}
