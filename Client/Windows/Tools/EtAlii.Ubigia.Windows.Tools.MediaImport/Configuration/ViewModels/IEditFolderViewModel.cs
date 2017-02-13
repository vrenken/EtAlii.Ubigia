namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;
    using System.Windows.Input;

    internal interface IEditFolderViewModel : INotifyPropertyChanged
    {
        ICommand CancelChangesCommand { get; }
        string LocalFolder { get; set; }
        IFolderMonitor OriginalFolderMonitor { get; set; }
        string RemoteName { get; set; }
        ICommand RemoveFolderCommand { get; }
        ICommand SaveChangesCommand { get; }
        ICommand SelectFolderCommand { get; }
    }
}