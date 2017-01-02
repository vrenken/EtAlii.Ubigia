namespace EtAlii.Servus.Api.Transport
{
    public class TransportStub : ITransport
    {
        public void Open(string address)
        {
        }

        public void Close()
        {
        }

        public void Register(IDataClient client)
        {
        }

        public void Register(INotificationClient client)
        {
        }
    }
}
