namespace EtAlii.Servus.Api.Transport
{
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRNotificationTransport : INotificationTransport 
    {
        public HubConnection HubConnection { get { return _hubConnection; } }
        private HubConnection _hubConnection;

        private readonly IHttpClient _signalRHttpClient;

        private readonly List<INotificationClient> _clients = new List<INotificationClient>();

        public static SignalRNotificationTransport CreateDefault()
        {
            return new SignalRNotificationTransport(new DefaultHttpClient());   
        }

        public SignalRNotificationTransport(IHttpClient signalRHttpClient)
        {
            _signalRHttpClient = signalRHttpClient;
        }

        public void Open(string address)
        {
            if (_hubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToNotifications);
            }
            _hubConnection = new HubConnectionFactory().Create(address + RelativeUri.Data);

            _hubConnection.Start(_signalRHttpClient).Wait();
        }

        public void Close()
        {
            if (_hubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToNotifications);
            }

            foreach (var client in _clients)
            {
                client.Disconnect();
            }

            _hubConnection.Dispose();
            _hubConnection = null;
        }

        public void Register(INotificationClient client)
        {
            _clients.Add(client);
        }
    }
}
