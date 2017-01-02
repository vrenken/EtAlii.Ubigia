namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using System.ComponentModel;

    internal interface IItemChecker : INotifyPropertyChanged
    {
        FolderSyncConfiguration Configuration { get; set; }

        bool IsRunning { get; }
        void Start();
        void Stop();
        void Enqueue(ItemCheckAction action);
    }
}
