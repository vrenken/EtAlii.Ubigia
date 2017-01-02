namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using Microsoft.AspNet.SignalR.Client;
    using System;
    using System.Collections.Generic;

    public class SignalRContentNotificationClient : SignalRNotificationClientBase, IContentNotificationClient
    {
        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public SignalRContentNotificationClient(INotificationTransport notificationTransport)
            : base((SignalRNotificationTransport)notificationTransport, "ContentHub")
        {
        }

        protected override IEnumerable<IDisposable> CreateSubscriptions(IHubProxy hubProxy)
        {
            return new IDisposable[] 
            {
                hubProxy.On<Identifier>("updated", OnUpdated),
                hubProxy.On<Identifier>("stored", OnStored),
            };
        }

        private void OnUpdated(Identifier identifier)
        {
            Updated(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
