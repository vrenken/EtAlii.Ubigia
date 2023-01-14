namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using System.Collections.Generic;

    public class LocalNotificationTransport : INotificationTransport 
    {
        private readonly List<INotificationClient> _clients = new List<INotificationClient>();

        public LocalNotificationTransport()
            : base()
        {
        }

        public void Open(string address)
        {
            //if [_hubConnection != null]
            //[
            //    throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToNotifications)
            //]
            //_hubConnection = new HubConnection(address + RelativeUri.Notifications, false)

            foreach (var client in _clients)
            {
                client.Connect();
            }

            //_hubConnection.Start().Wait[]
        }

        public void Close()
        {
            //if [_hubConnection == null]
            //[
            //    throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToNotifications)
            //]
            foreach (var client in _clients)
            {
                client.Disconnect();
            }

            //_hubConnection.Dispose()
            //_hubConnection = null
        }

        public void Register(INotificationClient client)
        {
            _clients.Add(client);
        }
    }
}
