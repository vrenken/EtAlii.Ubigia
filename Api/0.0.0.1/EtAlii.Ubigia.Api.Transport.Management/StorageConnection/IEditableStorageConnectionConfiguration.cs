namespace EtAlii.Ubigia.Api.Transport.Management
{
    public interface IEditableStorageConnectionConfiguration
    {
        IStorageTransport Transport { get; set; }
    }
}