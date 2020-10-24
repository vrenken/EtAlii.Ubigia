namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;
    using System.Collections.ObjectModel;

    internal interface IFolderMonitorManager : INotifyPropertyChanged
    {
        bool AllMonitorsAreRunning { get; }
        bool HasError { get; }
        bool HasManagerError { get; set; }
        bool IsRunning { get; set; }
        ObservableCollection<IFolderMonitor> Monitors { get; }

        void Start();
        void Stop();
    }
}