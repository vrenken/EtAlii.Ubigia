namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Net.Http;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.Sockets.Client;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
        public HubConnection Create(string address, HttpClientHandler httpClientHandler)
        {
            return new HubConnectionBuilder()
                .WithUrl(address)
                .WithMessageHandler(httpClientHandler)
                .WithJsonProtocol((JsonSerializer) new SerializerFactory().Create())
                .Build();
        }
    }
}
