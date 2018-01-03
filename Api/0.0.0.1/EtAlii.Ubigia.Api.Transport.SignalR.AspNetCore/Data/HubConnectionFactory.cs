namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
        public HubConnection Create(string address, ClientHttpMessageHandler httpClientHandler)
        {
            return new HubConnectionBuilder()
                .WithUrl(address)
                .WithMessageHandler(httpClientHandler)
                .WithJsonProtocol((JsonSerializer) new SerializerFactory().Create())
                .Build();
        }
    }
}
