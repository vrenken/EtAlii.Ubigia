namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Threading.Tasks;

    internal interface IItemUpdater
    {
        FolderSyncConfiguration Configuration { get; set; }

        Task Update(ItemCheckAction action);
    }
}
