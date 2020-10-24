namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Windows.Mvvm;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class ConfigurationViewModel : BindableBase, IConfigurationViewModel
    {
        private readonly Container _container;

        public ConfigurationViewModel(
            Container container,
            IObservableFolderSyncConfigurationCollection folderSyncConfigurations,
            IFolderMonitorManager manager, 
            IDataConnection connection)
        {
            _container = container;
            FolderSyncConfigurations = folderSyncConfigurations;
            Manager = manager;
            Connection = connection;

            SelectSpaceCommand = new RelayCommand(OnSelectSpace, CanSelectSpace);
            AddFolderCommand = new RelayCommand(OnAddFolder, CanAddFolder);
            EditFolderCommand = new RelayCommand(OnEditFolder, CanEditFolder);
        }
    }
}