namespace EtAlii.Servus.Api.Transport
{
    public class NotificationTransportStub : INotificationTransport 
    {
        public void Open(string address)
        {
        }

        public void Close()
        {
        }

        public void Register(INotificationClient client)
        { 
        }
    }
}
