namespace EtAlii.Ubigia.Api.Transport.Management
{
    public class StorageConnectionConfiguration : Configuration<StorageConnectionConfiguration>, IStorageConnectionConfiguration, IEditableStorageConnectionConfiguration
    {
        IStorageTransport IEditableStorageConnectionConfiguration.Transport { get => Transport; set => Transport = value; }
        public IStorageTransport Transport { get; private set; }

        IStorageConnectionConfiguration IConfiguration<IStorageConnectionConfiguration>.Use(IExtension[] extensions)
        {
            return base.Use(extensions);
        }
    }
}