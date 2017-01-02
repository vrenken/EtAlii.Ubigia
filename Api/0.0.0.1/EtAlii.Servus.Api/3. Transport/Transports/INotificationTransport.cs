namespace EtAlii.Servus.Api.Transport
{
    public interface INotificationTransport
    {
        void Open(string address);
        void Close();

        void Register(INotificationClient client);
    }
}
