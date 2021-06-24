// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Infrastructure.Transport
//[
//    using EtAlii.Ubigia.Api.Transport
//    using EtAlii.Ubigia.Api.Transport.Rest
//    using EtAlii.Ubigia.Infrastructure

//    internal class SystemConnectionClientsScaffolding : IScaffolding
//    [
//        public void Register(Container container)
//        [
//            var isRestTransport = container.GetInstance<ITransport>() is RestTransport
//            if [isRestTransport]
//            [
//                container.Register<IStorageClient, RestStorageClient>()
//                container.Register<IAccountClient, RestAccountClient>()
//                container.Register<ISpaceClient, RestSpaceClient>()
//            ]
//            else
//            [
//                container.Register<IStorageClient, StorageClientStub>()
//                container.Register<IAccountClient, AccountClientStub>()
//                container.Register<ISpaceClient, SpaceClientStub>()
//            ]
//        ]
//    ]
//]
