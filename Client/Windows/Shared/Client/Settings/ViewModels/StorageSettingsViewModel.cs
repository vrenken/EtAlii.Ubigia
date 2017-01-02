namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    using EtAlii.xTechnology.Mvvm;

    public class StorageSettingsViewModel : BindableBase
    {
        public GlobalSettings GlobalSettings { get { return _globalSettings; } }
        private readonly GlobalSettings _globalSettings;

        public StorageSettings StorageSettings { get { return _storageSettings; } set { SetProperty(ref _storageSettings, value); } }
        private StorageSettings _storageSettings;

        public bool StorageCanBeRemoved { get { return _storageCanBeRemoved; } set { SetProperty(ref _storageCanBeRemoved, value); } }
        private bool _storageCanBeRemoved = true;

        public StorageSettingsViewModel(GlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }
    }
}
