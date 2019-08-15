namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Windows.Mvvm;
    using MessageBox = System.Windows.Forms.MessageBox;

    internal class EditFolderViewModel : BindableBase, IEditFolderViewModel
    {
        private readonly IDataConnection _connection;
        private readonly IObservableFolderSyncConfigurationCollection _folderSyncConfigurations;

        public ICommand SaveChangesCommand { get; }

        public ICommand CancelChangesCommand { get; }

        public ICommand RemoveFolderCommand { get; }

        public ICommand SelectFolderCommand { get; }

        public IFolderMonitor OriginalFolderMonitor { get => _originalFolderMonitor; set => SetProperty(ref _originalFolderMonitor, value); }
        private IFolderMonitor _originalFolderMonitor;

        public string LocalFolder { get => _localFolder; set => SetProperty(ref _localFolder, value); }
        private string _localFolder;

        public string RemoteName { get => _remoteName; set => SetProperty(ref _remoteName, value); }
        private string _remoteName;

        public EditFolderViewModel(IDataConnection connection, IObservableFolderSyncConfigurationCollection folderSyncConfigurations)
        {
            _connection = connection;
            _folderSyncConfigurations = folderSyncConfigurations;

            SaveChangesCommand = new RelayCommand(OnSaveChanges, CanSaveChanges);
            CancelChangesCommand = new RelayCommand(OnCancelChanges, CanCancelChanges);
            RemoveFolderCommand = new RelayCommand(OnRemoveFolder, CanRemoveFolder);

            SelectFolderCommand = new RelayCommand(OnSelectFolder, CanSelectFolder);
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "OriginalFolderMonitor":
                    if (OriginalFolderMonitor != null)
                    {
                        LocalFolder = OriginalFolderMonitor.Configuration.LocalFolder;
                        RemoteName = OriginalFolderMonitor.Configuration.RemoteName;
                    }
                    break;
            }
        }

        private bool CanRemoveFolder(object obj)
        {
            return OriginalFolderMonitor != null;
        }

        private void OnRemoveFolder(object obj)
        {
            var window = (Window)obj;
            var win32Window = window.GetWin32Window();
            var dr = MessageBox.Show(win32Window, "Do you really want to stop synchronizing this folder?", "Remove synchronization", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                OriginalFolderMonitor.Configuration.Delete();
                window.DialogResult = false;
            }
        }

        private bool CanCancelChanges(object obj)
        {
            return true;//LocalFolder != OriginalFolderMonitor.Configuration.LocalFolder ||
                   //RemoteName != OriginalFolderMonitor.Configuration.RemoteName
        }

        private void OnCancelChanges(object obj)
        {
            var window = (Window)obj;
            window.DialogResult = false;
        }

        private bool CanSaveChanges(object obj)
        {
            var localFolder = LocalFolder != null ? LocalFolder.ToLower() : string.Empty;
            var remoteName = RemoteName != null ? RemoteName.ToLower() : string.Empty;

            var hasData = !string.IsNullOrWhiteSpace(localFolder) &&
                          !string.IsNullOrWhiteSpace(remoteName);
            var folderExists = Directory.Exists(localFolder);
            var nameConflicts = _folderSyncConfigurations.Where(c => c.RemoteName.ToLower() == remoteName);
            var noNameConflicts = OriginalFolderMonitor != null ? nameConflicts.All(c => c == OriginalFolderMonitor.Configuration) : !nameConflicts.Any();
            var nameIsCorrect = remoteName.StartsWith("/") && !remoteName.EndsWith("/") && !remoteName.Contains("\"");

            var folderConflicts = _folderSyncConfigurations.Where(c =>
            {
                var cLocalFolder = c.LocalFolder.ToLower();
                return cLocalFolder.Contains(localFolder) ||
                       localFolder.Contains(cLocalFolder) ||
                       cLocalFolder == localFolder;
            });
            var noFolderConflicts = OriginalFolderMonitor != null ? folderConflicts.All(c => c == OriginalFolderMonitor.Configuration) : !folderConflicts.Any();

            return hasData && folderExists && noNameConflicts && nameIsCorrect && noFolderConflicts;
        }

        private void OnSaveChanges(object obj)
        {
            var window = (Window)obj;

            if (OriginalFolderMonitor != null)
            {
                OriginalFolderMonitor.Configuration.LocalFolder = LocalFolder;
                OriginalFolderMonitor.Configuration.RemoteName = RemoteName;
                OriginalFolderMonitor.Configuration.Save();
            }
            else
            {
                var folderSyncConfiguration = new FolderSyncConfiguration(_folderSyncConfigurations);
                folderSyncConfiguration.LocalFolder = LocalFolder;
                folderSyncConfiguration.RemoteName = RemoteName;
                folderSyncConfiguration.Save();
            }
            window.DialogResult = false;
        }

        private bool CanSelectFolder(object obj)
        {
            return true;
        }

        private void OnSelectFolder(object obj)
        {
            var window = (Window)obj;
            var dialog = new FolderBrowserDialog
            {
                Description = @"Select the folder that should be monitored",
                SelectedPath = LocalFolder,
                
            };
            var win32Window = window.GetWin32Window();
            var dr = dialog.ShowDialog(win32Window);
            if (dr == DialogResult.OK)
            {
                LocalFolder = dialog.SelectedPath;
            }
        }
    }
}