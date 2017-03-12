﻿namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Windows.Tools.MediaImport.Properties;
    using EtAlii.xTechnology.Mvvm;

    public class FolderSyncConfiguration : BindableBase
    {
        private readonly IObservableFolderSyncConfigurationCollection _folderSyncConfigurations;

        public FolderSyncConfiguration(IObservableFolderSyncConfigurationCollection folderSyncConfigurations)
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
