namespace EtAlii.Servus.Api
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
