using EtAlii.xTechnology.MicroContainer;
using System;
namespace EtAlii.Servus.Api
{

    public class CollectionBase<T,N,D>
        where N : INotificationClient
        where D : IDataClient
    {
        protected T Connection { get { return _connection.Value; } }
        private readonly Lazy<T> _connection;

        protected N Notifications { get { return _notifications; } }
        private readonly N _notifications;

        protected D Data { get { return _data; } }
        private readonly D _data;

        protected CollectionBase(Container container, IAddressFactory addressFactory, N notificationClient, D dataClient)
        {
            _connection = new Lazy<T>(() => container.GetInstance<T>());
            _notifications = notificationClient;
            _data = dataClient;
        }
    }
}
