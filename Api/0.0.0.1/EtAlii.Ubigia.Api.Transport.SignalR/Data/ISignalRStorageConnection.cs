namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using EtAlii.Ubigia.Api.Management.SignalR;

    public interface ISignalRStorageConnection : ISignalRConnection, IStorageConnection<ISignalRStorageTransport>
    {
    }
}