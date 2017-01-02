//namespace EtAlii.Servus.Api.Transport
//{
//    using System;
//    using System.Collections.Generic;
//    using Microsoft.AspNet.SignalR.Client;

//    internal abstract class __SignalRNotificationClientBase : INotificationClient
//    {
//        private readonly SignalRNotificationTransport _notificationTransport;
//        private IHubProxy _notificationProxy;
//        private readonly string _hubName;
//        private IEnumerable<IDisposable> _subscriptions;

//        protected __SignalRNotificationClientBase(SignalRNotificationTransport notificationTransport, string hubName)
//        {
//            _hubName = hubName;

//            _notificationTransport = notificationTransport;
//            _notificationTransport.Register(this);
//        }
         
//        public void Connect()
//        {
//            _notificationProxy = _notificationTransport.HubConnection.CreateHubProxy(_hubName);
//            _subscriptions = CreateSubscriptions(_notificationProxy);
//        }

//        public void Disconnect()
//        {
//            foreach (var subscription in _subscriptions)
//            {
//                subscription.Dispose();
//            }
//            _notificationProxy = null;
//        }
//    }
//}
