//namespace EtAlii.Servus.Api.Transport
//{
//    using System.Linq;
//    using EtAlii.xTechnology.MicroContainer;

//    public class TransportClientRegistrationScaffolding : IScaffolding
//    {
//        public void Register(Container container)
//        {
//            container.RegisterInitializer<IAuthenticationDataClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IEntryDataClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IRootDataClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IContentDataClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IPropertiesDataClient>(client => container.GetInstance<ITransport>().Register(client));

//            container.RegisterInitializer<IEntryNotificationClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IRootNotificationClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IContentNotificationClient>(client => container.GetInstance<ITransport>().Register(client));
//            container.RegisterInitializer<IPropertiesNotificationClient>(client => container.GetInstance<ITransport>().Register(client));
//        }
//    }
//}