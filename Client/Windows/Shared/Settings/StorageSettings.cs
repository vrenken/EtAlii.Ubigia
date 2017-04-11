using System;

namespace EtAlii.Ubigia.Client.Windows
{
    public class StorageSettings : BindableSettingsBase
    {
        public bool MountAsStorage { get { return GetValue(ref _mountAsStorage, true); } set { SetProperty(ref _mountAsStorage, value); } }
        private Nullable<bool> _mountAsStorage;

        public string Name { get { return GetValue(ref _name, $"Unnamed {Settings.StorageNaming}"); } set { SetProperty(ref _name, value); } }
        private string _name;

        public Guid Id { get; }

        public string Account { get { return GetValue(ref _account, null); } set { SetProperty(ref _account, value); } }
        private string _account;

        public bool IsHostedStorage { get { return GetValue(ref _isHostedStorage, false); } set { SetProperty(ref _isHostedStorage, value); } }
        private Nullable<bool> _isHostedStorage;

        public string HostingUrl { get { return GetValue(ref _hostingUrl, "https://"); } set { SetProperty(ref _hostingUrl, value); } }
        private string _hostingUrl;

        public string StorageLocation { get { return GetValue(ref _storageLocation, "c:\\"); } set { SetProperty(ref _storageLocation, value); } }
        private string _storageLocation;

        public bool UseDataEncryption { get { return GetValue(ref _useDataEncryption, false); } set { SetProperty(ref _useDataEncryption, value); } }
        private Nullable<bool> _useDataEncryption;

        public double UsedCapacity { get { return GetValue(ref _usedCapacity, new Random().NextDouble()); } set { SetProperty(ref _usedCapacity, value); } }
        private Nullable<double> _usedCapacity;

        public StorageSettings(string id)
            : base($"{Settings.StoragesNaming}\\{id}")
        {
            Id = new Guid(id);
        }
    }
}
