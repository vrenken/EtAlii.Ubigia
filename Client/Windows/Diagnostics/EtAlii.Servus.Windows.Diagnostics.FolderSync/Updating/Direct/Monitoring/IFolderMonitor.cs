namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using System;
    using System.ComponentModel;

    internal interface IFolderMonitor : INotifyPropertyChanged
    {
        FolderSyncConfiguration Configuration { get; set; }
        bool IsRunning { get; }
        bool HasError { get; set; }
        event EventHandler Changed;
        event EventHandler Error;
        void Start();
        void Stop();
    }
}