namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using EtAlii.Ubigia.Client.Windows.Shared;
    using EtAlii.xTechnology.Mvvm;
    using Fluent;

    internal partial class ConfigurationViewModel : BindableBase
    {
        public ICommand AddFolderCommand { get { return _addFolderCommand; } }
        private readonly ICommand _addFolderCommand;

        public ICommand EditFolderCommand { get { return _editFolderCommand; } }
        private readonly ICommand _editFolderCommand;

        public FolderMonitorManager Manager { get { return _manager; } }
        private readonly FolderMonitorManager _manager;

        public FolderMonitor SelectedFolderMonitor { get { return _selectedFolderMonitor; } set { SetProperty(ref _selectedFolderMonitor, value); } }
        private FolderMonitor _selectedFolderMonitor;

        public ObservableCollection<FolderSyncConfiguration> FolderSyncConfigurations { get { return _folderSyncConfigurations; } }
        private ObservableCollection<FolderSyncConfiguration> _folderSyncConfigurations;

        private void OnAddFolder(object obj)
        {
            var parentWindow = (RibbonWindow)obj;
            var window = _container.GetInstance<EditFolderWindow>();
            window.Owner = parentWindow;
            window.ShowDialog();
        }

        private bool CanAddFolder(object obj)
        {
            return true;
        }

        private void OnEditFolder(object obj)
        {
            var parentWindow = (RibbonWindow)obj;
            var window = _container.GetInstance<EditFolderWindow>();
            window.Owner = parentWindow;
            ((EditFolderViewModel) window.DataContext).OriginalFolderMonitor = SelectedFolderMonitor;
            window.ShowDialog();
        }

        private bool CanEditFolder(object obj)
        {
            return _selectedFolderMonitor != null;
        }
    }
}