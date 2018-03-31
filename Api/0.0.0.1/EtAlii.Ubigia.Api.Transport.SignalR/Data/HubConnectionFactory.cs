namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System;
	using Microsoft.AspNet.SignalR.Client;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
        public HubConnection Create(Uri address)
        {
            return new HubConnection(address.ToString(), false)
            {
                JsonSerializer = (JsonSerializer) new SerializerFactory().Create()
            };
        }
    }
}
