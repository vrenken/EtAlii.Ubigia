namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Windows.Input;
    using Fluent;

    internal partial class ConfigurationViewModel
    {
        public ICommand AddFolderCommand { get; }

        public ICommand EditFolderCommand { get; }

        public IFolderMonitorManager Manager { get; }

        public IFolderMonitor SelectedFolderMonitor { get => _selectedFolderMonitor; set => SetProperty(ref _selectedFolderMonitor, value); }
        private IFolderMonitor _selectedFolderMonitor;

        public IObservableFolderSyncConfigurationCollection FolderSyncConfigurations { get; }

        private void OnAddFolder(object obj)
        {
            var parentWindow = (RibbonWindow)obj;
            var window = _container.GetInstance<IEditFolderWindow>();
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
            var window = _container.GetInstance<IEditFolderWindow>();
            window.Owner = parentWindow;
            ((IEditFolderViewModel) window.DataContext).OriginalFolderMonitor = SelectedFolderMonitor;
            window.ShowDialog();
        }

        private bool CanEditFolder(object obj)
        {
            return _selectedFolderMonitor != null;
        }
    }
}