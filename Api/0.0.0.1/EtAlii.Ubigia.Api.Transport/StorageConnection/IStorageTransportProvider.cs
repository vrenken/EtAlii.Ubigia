namespace EtAlii.Ubigia.Api.Transport
{
    public interface IStorageTransportProvider : ITransportProvider
    {
        IStorageTransport GetStorageTransport();
    }
}