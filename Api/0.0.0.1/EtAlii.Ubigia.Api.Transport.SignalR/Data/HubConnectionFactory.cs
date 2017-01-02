namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using Microsoft.AspNet.SignalR.Client;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
        public HubConnection Create(string address)
        {
            return new HubConnection(address, false)
            {
                JsonSerializer = (JsonSerializer) new SerializerFactory().Create()
            };
        }
    }
}
