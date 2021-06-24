// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class GrpcSpaceClientsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ISpaceConnection, GrpcSpaceConnection>();

            container.Register<IAuthenticationDataClient, GrpcAuthenticationDataClient>();

            container.Register<IEntryDataClient, GrpcEntryDataClient>();
            container.Register<IEntryNotificationClient, GrpcEntryNotificationClient>();

            container.Register<IRootDataClient, GrpcRootDataClient>();
            container.Register<IRootNotificationClient, GrpcRootNotificationClient>();

            container.Register<IPropertiesDataClient, GrpcPropertiesDataClient>();
            container.Register<IPropertiesNotificationClient, GrpcPropertiesNotificationClient>();

            container.Register<IContentDataClient, GrpcContentDataClient>();
            container.Register<IContentNotificationClient, GrpcContentNotificationClient>();
            
            // The GrpcPropertiesDataClient requires advanced serialization.
            container.Register(() => new SerializerFactory().Create());
        }
    }
}
