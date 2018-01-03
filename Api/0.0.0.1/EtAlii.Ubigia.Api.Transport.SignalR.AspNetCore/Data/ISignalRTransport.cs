namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Net.Http;
    using Microsoft.AspNetCore.SignalR.Client;

    public interface ISignalRTransport
    {
        ClientHttpMessageHandler HttpClientHandler { get; }
        HubConnection HubConnection { get; }

        string AuthenticationToken { get; set; }
    }
}