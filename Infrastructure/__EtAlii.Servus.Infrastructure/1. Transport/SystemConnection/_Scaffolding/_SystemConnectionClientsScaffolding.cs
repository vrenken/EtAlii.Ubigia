//namespace EtAlii.Servus.Infrastructure.Transport
//{
//    using EtAlii.Servus.Api.Transport;
//    using EtAlii.Servus.Api.Transport.WebApi;
//    using EtAlii.Servus.Infrastructure;

//    internal class SystemConnectionClientsScaffolding : IScaffolding
//    {
//        public void Register(Container container)
//        {
//            var isWebApiTransport = container.GetInstance<ITransport>() is WebApiTransport;
//            if (isWebApiTransport)
//            {
//                container.Register<IStorageClient, WebApiStorageClient>();
//                container.Register<IAccountClient, WebApiAccountClient>();
//                container.Register<ISpaceClient, WebApiSpaceClient>();
//            }
//            else
//            {
//                container.Register<IStorageClient, StorageClientStub>();
//                container.Register<IAccountClient, AccountClientStub>();
//                container.Register<ISpaceClient, SpaceClientStub>();
//            }
//        }

//    }
//}
