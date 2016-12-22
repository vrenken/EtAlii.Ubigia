namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Servus.Diagnostics.FolderSync.Properties;
    using EtAlii.xTechnology.Mvvm;

    public class FolderSyncConfiguration : BindableBase
    {
        private readonly ObservableCollection<FolderSyncConfiguration> _folderSyncConfigurations;

        public FolderSyncConfiguration(ObservableCollection<FolderSyncConfiguration> folderSyncConfigurations)
        {
            _folderSyncConfigurations = folderSyncConfigurations;
        }

        public string LocalFolder { get { return _localFolder; } set { SetProperty(ref _localFolder, value); } }
        private string _localFolder;

        public string RemoteName { get { return _remoteName; } set { SetProperty(ref _remoteName, value); }}
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
                .Select(f => String.Format("{0}|{1}", f.LocalFolder, f.RemoteName))
                .ToArray();
            Settings.Default.Folders.Clear();
            Settings.Default.Folders.AddRange(foldersAsStrings);
            Settings.Default.Save();
        }
    }
}
