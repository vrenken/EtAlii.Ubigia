namespace EtAlii.Servus.Api.Transport.SignalR
{
    using EtAlii.Servus.Api.Management.SignalR;

    public interface ISignalRStorageConnection : ISignalRConnection, IStorageConnection<ISignalRStorageTransport>
    {
    }
}