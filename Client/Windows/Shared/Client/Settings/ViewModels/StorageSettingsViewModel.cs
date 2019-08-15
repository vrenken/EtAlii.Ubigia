namespace EtAlii.Ubigia.Windows.Client
{
    using EtAlii.Ubigia.Windows.Mvvm;
    using EtAlii.Ubigia.Windows.Settings;

    public class StorageSettingsViewModel : BindableBase
    {
        public IGlobalSettings GlobalSettings { get; }

        public StorageSettings StorageSettings { get { return _storageSettings; } set { SetProperty(ref _storageSettings, value); } }
        private StorageSettings _storageSettings;

        public bool StorageCanBeRemoved { get { return _storageCanBeRemoved; } set { SetProperty(ref _storageCanBeRemoved, value); } }
        private bool _storageCanBeRemoved = true;

        public StorageSettingsViewModel(IGlobalSettings globalSettings)
        {
            GlobalSettings = globalSettings;
        }
    }
}
