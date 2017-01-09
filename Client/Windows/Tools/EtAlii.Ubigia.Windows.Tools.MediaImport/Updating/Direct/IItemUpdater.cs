namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;

    internal interface IItemUpdater
    {
        FolderSyncConfiguration Configuration { get; set; }

        void Update(ItemCheckAction action);
    }
}
