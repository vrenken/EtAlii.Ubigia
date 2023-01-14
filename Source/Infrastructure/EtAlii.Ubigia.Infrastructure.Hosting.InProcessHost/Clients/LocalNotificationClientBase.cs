namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;

    public abstract class LocalNotificationClientBase : NotificationClientBase<LocalNotificationTransport>, INotificationClient
    {
        private object _subscriptions;
        private object _notificationProxy;

        protected LocalNotificationClientBase(LocalNotificationTransport notificationTransport)
            : base(notificationTransport)
        {
            notificationTransport.Register(this);
        }

        protected abstract object CreateSubscriptions(object proxy);

        public void Connect()
        {
            //_notificationProxy = NotificationTransport.HubConnection.CreateHubProxy(_hubName)
            _subscriptions = CreateSubscriptions(_notificationProxy);
        }

        public void Disconnect()
        {
            //foreach (var subscription in _subscriptions)
            //[
            //    subscription.Dispose()
            //]
            _notificationProxy = null;
        }
    }
}
