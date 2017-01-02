namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
using Microsoft.AspNet.SignalR.Client;
    using System;
using System.Collections.Generic;

    public class SignalREntryNotificationClient : SignalRNotificationClientBase, IEntryNotificationClient
    {
        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public SignalREntryNotificationClient(INotificationTransport notificationTransport)
            : base((SignalRNotificationTransport)notificationTransport, "EntryHub")
        {
        }

        protected override IEnumerable<IDisposable> CreateSubscriptions(IHubProxy hubProxy)
        {
            return new IDisposable[] 
            {
                hubProxy.On<Identifier>("prepared", OnPrepared),
                hubProxy.On<Identifier>("stored", OnStored),
            };
        }

        private void OnPrepared(Identifier identifier)
        {
            Prepared(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
