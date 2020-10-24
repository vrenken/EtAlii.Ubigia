namespace EtAlii.Ubigia.Windows.Settings
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class GlobalSettings : BindableSettingsBase, IGlobalSettings
    {
        public bool StartAutomatically { get => GetValue(ref _startAutomatically, true); set => SetProperty(ref _startAutomatically, value); }
        private bool? _startAutomatically;

        public bool RememberLoginCredentials { get => GetValue(ref _rememberLoginCredentials, false); set => SetProperty(ref _rememberLoginCredentials, value); }
        private bool? _rememberLoginCredentials;

        public bool AutomaticallySendLogFiles { get => GetValue(ref _automaticallySendLogFiles, true); set => SetProperty(ref _automaticallySendLogFiles, value); }
        private bool? _automaticallySendLogFiles;

        public string ConsoleTarget { get => GetValue(ref _consoleTarget, null);
            set => SetProperty(ref _consoleTarget, value);
        }
        private string _consoleTarget;

        public ObservableCollection<StorageSettings> Storage { get; } = new ObservableCollection<StorageSettings>();

        public GlobalSettings()
        {
            using (var storagesKey = GetSubKey(Settings.StoragesNaming))
            {
                foreach (var storageKey in storagesKey.GetSubKeyNames())
                {
                    var storageSettings = new StorageSettings(storageKey);
                    Storage.Add(storageSettings);
                }
            }

            Storage.CollectionChanged += OnStorageSettingsChanged;
        }

        void OnStorageSettingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    foreach (StorageSettings storageSettings in e.OldItems)
                    {
                        storageSettings.Delete();
                    }
                    break;
            }
        }
    }
}
