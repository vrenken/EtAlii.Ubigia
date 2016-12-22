namespace EtAlii.Servus.Api.Transport
{
    public interface IStorageTransportProvider : ITransportProvider
    {
        IStorageTransport GetStorageTransport();
    }
}