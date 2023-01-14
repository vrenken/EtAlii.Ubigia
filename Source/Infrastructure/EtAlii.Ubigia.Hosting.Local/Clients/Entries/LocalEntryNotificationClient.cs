namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using System;

    public class LocalEntryNotificationClient : LocalNotificationClientBase, IEntryNotificationClient
    {
        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public LocalEntryNotificationClient(INotificationTransport notificationTransport)
            : base((LocalNotificationTransport)notificationTransport)
        {
        }

        protected override object CreateSubscriptions(object hubProxy)
        {
            throw new System.NotImplementedException();

            //return new IDisposable[] 
            //[
            //    hubProxy.On<Identifier>("prepared", OnPrepared),
            //    hubProxy.On<Identifier>("stored", OnStored),
            //]
        }

        private void OnPrepared(in Identifier identifier)
        {
            throw new System.NotImplementedException();

            //Prepared(identifier)
        }

        private void OnStored(in Identifier identifier)
        {
            throw new System.NotImplementedException();

            //Stored(identifier)
        }
    }
}
