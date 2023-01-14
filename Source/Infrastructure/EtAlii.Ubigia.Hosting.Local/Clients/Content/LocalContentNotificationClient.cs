namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using System;

    public class LocalContentNotificationClient : LocalNotificationClientBase, IContentNotificationClient
    {
        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public LocalContentNotificationClient(INotificationTransport notificationTransport)
            : base((LocalNotificationTransport)notificationTransport)
        {
        }

        protected override object CreateSubscriptions(object hubProxy)
        {
            throw new System.NotImplementedException();

            //return new IDisposable[] 
            //[
            //    hubProxy.On<Identifier>("updated", OnUpdated),
            //    hubProxy.On<Identifier>("stored", OnStored),
            //]
        }

        private void OnUpdated(in Identifier identifier)
        {
            throw new System.NotImplementedException();

            //Updated(identifier)
        }

        private void OnStored(in Identifier identifier)
        {
            throw new System.NotImplementedException();

            //Stored(identifier)
        }
    }
}
