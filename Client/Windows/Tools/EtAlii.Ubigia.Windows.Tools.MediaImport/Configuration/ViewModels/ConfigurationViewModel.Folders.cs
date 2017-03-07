namespace EtAlii.Ubigia.Windows.Tools.MediaImport
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
        public ICommand AddFolderCommand => _addFolderCommand;
        private readonly ICommand _addFolderCommand;

        public ICommand EditFolderCommand => _editFolderCommand;
        private readonly ICommand _editFolderCommand;

        public IFolderMonitorManager Manager => _manager;
        private readonly IFolderMonitorManager _manager;

        public IFolderMonitor SelectedFolderMonitor { get { return _selectedFolderMonitor; } set { SetProperty(ref _selectedFolderMonitor, value); } }
        private IFolderMonitor _selectedFolderMonitor;

        public IObservableFolderSyncConfigurationCollection FolderSyncConfigurations => _folderSyncConfigurations;
        private readonly IObservableFolderSyncConfigurationCollection _folderSyncConfigurations;

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