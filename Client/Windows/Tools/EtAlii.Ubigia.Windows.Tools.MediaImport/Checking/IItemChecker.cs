namespace EtAlii.Ubigia.Windows.Tools.MediaImport
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
