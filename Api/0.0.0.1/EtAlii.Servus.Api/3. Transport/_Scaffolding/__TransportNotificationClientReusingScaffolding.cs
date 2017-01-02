//namespace EtAlii.Servus.Api.Transport
//{
//    using System.Linq;
//    using EtAlii.xTechnology.MicroContainer;

//    public class TransportNotificationClientReusingScaffolding : IScaffolding
//    {
//        private readonly ITransport _transport;

//        public TransportNotificationClientReusingScaffolding(ITransport transport)
//        {
//            _transport = transport;
//        }

//        public void Register(Container container)
//        {
//            var clients = _transport.Clients;
//            if (clients != null)
//            {
//                container.Register<IEntryNotificationClient>(() => clients.EntryNotifications);
//                container.Register<IRootNotificationClient>(() => clients.RootNotifications);
//                container.Register<IContentNotificationClient>(() => clients.ContentNotifications);
//                container.Register<IPropertiesNotificationClient>(() => clients.PropertiesNotifications);
//            }
//        }
//    }
//}