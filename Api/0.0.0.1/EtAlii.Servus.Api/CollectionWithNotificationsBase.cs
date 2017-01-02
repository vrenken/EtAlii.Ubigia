namespace EtAlii.Servus.Api
{

    //public abstract class CollectionWithNotificationsBase<T, N> : CollectionBase<T, N>
    //    where T : ConnectionBase
    //    where N : INotificationClient
    //{
    //    private IHubProxy _notificationProxy;
    //    private string _hubName;
    //    private IEnumerable<IDisposable> _subscriptions;


    //    protected CollectionWithNotificationsBase(T connection, INotificationClient notificationClient, string hubName)
    //        : base(connection, notificationClient)
    //    {
    //        _hubName = hubName;
    //    }
         
    //    protected abstract IEnumerable<IDisposable> CreateSubscriptions(IHubProxy proxy);

    //    public void Connect()
    //    {
    //        _notificationProxy = NotificationTransportClient NotificationConnection.CreateHubProxy(_hubName);
    //        _subscriptions = CreateSubscriptions(_notificationProxy);
    //    }

    //    public void Disconnect()
    //    {
    //        foreach (var subscription in _subscriptions)
    //        {
    //            subscription.Dispose();
    //        }
    //        _notificationProxy = null;
    //    }
    //}
}
