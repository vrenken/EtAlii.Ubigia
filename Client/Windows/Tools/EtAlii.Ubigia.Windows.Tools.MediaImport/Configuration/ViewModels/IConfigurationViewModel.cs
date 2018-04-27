// ReSharper disable UnusedMember.Global
namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport;

    internal interface IConfigurationViewModel : INotifyPropertyChanged
    {
        ICommand AddFolderCommand { get; }
        IDataConnection Connection { get; }
        ICommand EditFolderCommand { get; }
        IObservableFolderSyncConfigurationCollection FolderSyncConfigurations { get; }
        IFolderMonitorManager Manager { get; }
        IFolderMonitor SelectedFolderMonitor { get; set; }
        ICommand SelectSpaceCommand { get; }
    }
}