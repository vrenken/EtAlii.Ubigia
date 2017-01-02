//namespace EtAlii.Servus.Api.Transport
//{
//    using System.Linq;
//    using EtAlii.xTechnology.MicroContainer;

//    public class TransportClientsReusingScaffolding : IScaffolding
//    {
//        private readonly ITransport _transport;

//        public TransportClientsReusingScaffolding(ITransport transport)
//        {
//            _transport = transport;
//        }

//        public void Register(Container container)
//        {
//            var clients = _transport.Clients;
//            if (clients != null)
//            {
//                container.Register<IAuthenticationDataClient>(() => clients.AuthenticationData);

//                container.Register<IEntryDataClient>(() => clients.EntryData);
//                container.Register<IEntryNotificationClient>(() => clients.EntryNotifications);

//                container.Register<IRootDataClient>(() => clients.RootData);
//                container.Register<IRootNotificationClient>(() => clients.RootNotifications);

//                container.Register<IPropertiesDataClient>(() => clients.PropertiesData);
//                container.Register<IPropertiesNotificationClient>(() => clients.PropertiesNotifications);

//                container.Register<IContentDataClient>(() => clients.ContentData);
//                container.Register<IContentNotificationClient>(() => clients.ContentNotifications);
//            }
//        }
//    }
//}