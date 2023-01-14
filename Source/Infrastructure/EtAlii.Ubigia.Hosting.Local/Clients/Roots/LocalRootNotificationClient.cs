namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using System;

    public class LocalRootNotificationClient : LocalNotificationClientBase, IRootNotificationClient
    {
        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        public LocalRootNotificationClient(INotificationTransport notificationTransport) 
            : base((LocalNotificationTransport)notificationTransport)
        {
        }

        protected override object CreateSubscriptions(object hubProxy)
        {
            throw new System.NotImplementedException();

            //return new IDisposable[]
            //[
            //    hubProxy.On<Guid>("added", OnAdded),
            //    hubProxy.On<Guid>("changed", OnChanged),
            //    hubProxy.On<Guid>("removed", OnRemoved),
            //]
        }

        private void OnAdded(Guid id)
        {
            throw new System.NotImplementedException();

            //Added(id); 
        }

        private void OnChanged(Guid id)
        {
            throw new System.NotImplementedException();

            //Changed(id)
        }

        private void OnRemoved(Guid id)
        {
            throw new System.NotImplementedException();

            //Removed(id)
        }
    }
}
