namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    public interface IGrpcStorageConnection : IGrpcConnection, IStorageConnection<IGrpcStorageTransport>
    {
    }
}