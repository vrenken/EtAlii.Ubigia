namespace EtAlii.Servus.Api.Connection
{
    using EtAlii.Servus.Api.Connection.Transports;

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
