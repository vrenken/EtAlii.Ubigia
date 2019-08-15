namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Linq;
    using EtAlii.Ubigia.Windows.Mvvm;
    using EtAlii.Ubigia.Windows.Tools.MediaImport.Properties;

    public class FolderSyncConfiguration : BindableBase
    {
        private readonly IObservableFolderSyncConfigurationCollection _folderSyncConfigurations;

        public FolderSyncConfiguration(IObservableFolderSyncConfigurationCollection folderSyncConfigurations)
        {
            _folderSyncConfigurations = folderSyncConfigurations;
        }

        public string LocalFolder { get => _localFolder; set => SetProperty(ref _localFolder, value); }
        private string _localFolder;

        public string RemoteName { get => _remoteName; set => SetProperty(ref _remoteName, value); }
        private string _remoteName;

        public void Save()
        {
            if (!_folderSyncConfigurations.Contains(this))
            {
                _folderSyncConfigurations.Add(this);
            }
            SerializeToSettings();
        }

        public void Delete()
        {
            _folderSyncConfigurations.Remove(this);
            SerializeToSettings();
        }

        private void SerializeToSettings()
        {
            var foldersAsStrings = _folderSyncConfigurations
                .Select(f => $"{f.LocalFolder}|{f.RemoteName}")
                .ToArray();
            Settings.Default.Folders.Clear();
            Settings.Default.Folders.AddRange(foldersAsStrings);
            Settings.Default.Save();
        }
    }
}
