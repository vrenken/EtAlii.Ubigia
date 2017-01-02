namespace EtAlii.Servus.Api.Connection
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Connection.Transports;
    using Microsoft.AspNet.SignalR.Client;

    public abstract class SignalRNotificationClientBase : NotificationClientBase<SignalRNotificationTransport>, INotificationClient
    {
        private IHubProxy _notificationProxy;
        private string _hubName;
        private IEnumerable<IDisposable> _subscriptions;

        protected SignalRNotificationClientBase(SignalRNotificationTransport notificationTransport, string hubName)
            : base(notificationTransport)
        {
            _hubName = hubName;

            notificationTransport.Register(this);
        }
         
        protected abstract IEnumerable<IDisposable> CreateSubscriptions(IHubProxy proxy);

        public void Connect()
        {
            _notificationProxy = NotificationTransport.HubConnection.CreateHubProxy(_hubName);
            _subscriptions = CreateSubscriptions(_notificationProxy);
        }

        public void Disconnect()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
            _notificationProxy = null;
        }
    }
}
