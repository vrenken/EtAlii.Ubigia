namespace EtAlii.Servus.Api.Transport
{

    public interface INotificationClient
    {
        void Connect(ITransport transport);
        void Disconnect(ITransport transport);
    }
}
