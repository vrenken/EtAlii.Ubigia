namespace EtAlii.Servus.Api
{

    public abstract class NotificationClientBase<T>
        where T : INotificationTransport
    {
        protected T NotificationTransport { get { return _notificationTransport; } }
        private readonly T _notificationTransport;

        protected NotificationClientBase(INotificationTransport notificationTransport)
        {
            _notificationTransport = (T)notificationTransport;
        }
    }
}
