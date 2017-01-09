namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Mvvm;
    using SimpleInjector;

    internal partial class ConfigurationViewModel : BindableBase
    {
        private readonly Container _container;

        public ConfigurationViewModel(
            Container container,
            ObservableCollection<FolderSyncConfiguration> folderSyncConfigurations,
            FolderMonitorManager manager, 
            IDataConnection connection)
        {
            _container = container;
            _folderSyncConfigurations = folderSyncConfigurations;
            _manager = manager;
            _connection = connection;

            _selectSpaceCommand = new RelayCommand(OnSelectSpace, CanSelectSpace);
            _addFolderCommand = new RelayCommand(OnAddFolder, CanAddFolder);
            _editFolderCommand = new RelayCommand(OnEditFolder, CanEditFolder);
        }
    }
}