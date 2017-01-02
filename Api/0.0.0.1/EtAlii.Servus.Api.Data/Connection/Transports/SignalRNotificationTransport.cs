namespace EtAlii.Servus.Api
{
    using Microsoft.AspNet.SignalR.Client;
    using System.Collections.Generic;

    public class SignalRNotificationTransport : INotificationTransport 
    {
        public HubConnection HubConnection { get { return _hubConnection; } }
        private HubConnection _hubConnection;

        private readonly List<INotificationClient> _clients = new List<INotificationClient>();

        public void Open(string address)
        {
            if (_hubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToNotifications);
            }
            _hubConnection = new HubConnection(address + RelativeUri.Notifications, false);

            foreach (var client in _clients)
            {
                client.Connect();
            }

            _hubConnection.Start().Wait();
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
