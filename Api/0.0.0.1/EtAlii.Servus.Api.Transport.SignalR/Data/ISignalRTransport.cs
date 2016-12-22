namespace EtAlii.Servus.Api.Transport.SignalR
{
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public interface ISignalRTransport
    {
        IHttpClient HttpClient { get; }
        HubConnection HubConnection { get; }

        string AuthenticationToken { get; set; }
    }
}