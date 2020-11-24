namespace EtAlii.Ubigia.Windows.Settings
{
    using System;
    using System.Security.Cryptography;

    public class StorageSettings : BindableSettingsBase
    {
        private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        public StorageSettings()
        {
            var bytes = new byte[sizeof(double)];
            _random.GetNonZeroBytes(bytes);
            _usedCapacity = Convert.ToDouble(bytes); 
        }
        
        public bool MountAsStorage { get => GetValue(ref _mountAsStorage, true); set => SetProperty(ref _mountAsStorage, value); }
        private bool? _mountAsStorage;

        public string Name { get => GetValue(ref _name, $"Unnamed {Settings.StorageNaming}"); set => SetProperty(ref _name, value); }
        private string _name;

        public Guid Id { get; }

        public string Account { get => GetValue(ref _account, null); set => SetProperty(ref _account, value); }
        private string _account;

        public bool IsHostedStorage { get => GetValue(ref _isHostedStorage, false); set => SetProperty(ref _isHostedStorage, value); }
        private bool? _isHostedStorage;
        
        public string HostingUrl { get => GetValue(ref _hostingUrl, "https://"); set => SetProperty(ref _hostingUrl, value); }
        private string _hostingUrl;

        public string StorageLocation { get => GetValue(ref _storageLocation, "c:\\"); set => SetProperty(ref _storageLocation, value);
        }
        private string _storageLocation;

        public bool UseDataEncryption { get => GetValue(ref _useDataEncryption, false); set => SetProperty(ref _useDataEncryption, value); }
        private bool? _useDataEncryption;

        public double UsedCapacity { get => GetValue(ref _usedCapacity, 0); set => SetProperty(ref _usedCapacity, value); }
        private double? _usedCapacity;

        public StorageSettings(string id)
            : base($"{Settings.StoragesNaming}\\{id}")
        {
            Id = new Guid(id);
        }
    }
}
