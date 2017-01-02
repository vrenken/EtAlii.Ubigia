namespace EtAlii.Servus.Api
{

    public interface INotificationTransport
    {
        void Open(string address);
        void Close();

        void Register(INotificationClient client);
    }
}
