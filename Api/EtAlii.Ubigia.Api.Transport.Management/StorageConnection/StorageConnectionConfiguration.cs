namespace EtAlii.Ubigia.Api.Transport.Management
{
    public class StorageConnectionConfiguration : Configuration, IStorageConnectionConfiguration, IEditableStorageConnectionConfiguration
    {
        IStorageTransport IEditableStorageConnectionConfiguration.Transport { get => Transport; set => Transport = value; }
        public IStorageTransport Transport { get; private set; }
    }
}