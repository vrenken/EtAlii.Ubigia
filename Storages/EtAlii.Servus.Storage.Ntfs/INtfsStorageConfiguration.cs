namespace EtAlii.Servus.Storage.Ntfs
{
    public interface INtfsStorageConfiguration : IStorageConfiguration
    {
        string BaseFolder { get; set; }
    }
}