namespace EtAlii.Servus.Api.Transport.SignalR
{
    using Microsoft.AspNet.SignalR.Client;

    public interface ISignalRSpaceTransport : ISignalRTransport, ISpaceTransport
    {
        HubConnection HubConnection { get; }
    }
}