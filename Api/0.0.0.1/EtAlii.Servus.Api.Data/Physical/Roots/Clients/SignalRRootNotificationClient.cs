namespace EtAlii.Servus.Api.Data
{
    using Microsoft.AspNet.SignalR.Client;
    using System;
    using System.Collections.Generic;

    public class SignalRRootNotificationClient : SignalRNotificationClientBase, IRootNotificationClient
    {
        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        public SignalRRootNotificationClient(INotificationTransport notificationTransport) 
            : base((SignalRNotificationTransport)notificationTransport, "RootHub")
        {
        }

        protected override IEnumerable<IDisposable> CreateSubscriptions(IHubProxy hubProxy)
        {
            return new IDisposable[]
            {
                hubProxy.On<Guid>("added", OnAdded),
                hubProxy.On<Guid>("changed", OnChanged),
                hubProxy.On<Guid>("removed", OnRemoved),
            };
        }

        private void OnAdded(Guid id)
        {
            Added(id); 
        }

        private void OnChanged(Guid id)
        {
            Changed(id);
        }

        private void OnRemoved(Guid id)
        {
            Removed(id);
        }
    }
}
