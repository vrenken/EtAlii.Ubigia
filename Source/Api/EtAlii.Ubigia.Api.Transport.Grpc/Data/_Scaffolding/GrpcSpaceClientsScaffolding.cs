// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class GrpcSpaceClientsScaffolding : IScaffolding
    {
        private readonly SpaceConnectionOptions _options;

        public GrpcSpaceClientsScaffolding(SpaceConnectionOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<ISpaceConnection>(serviceCollection =>
            {
                var transport = serviceCollection.GetInstance<ISpaceTransport>();
                var roots = serviceCollection.GetInstance<IRootContext>();
                var entries = serviceCollection.GetInstance<IEntryContext>();
                var content = serviceCollection.GetInstance<IContentContext>();
                var properties = serviceCollection.GetInstance<IPropertiesContext>();
                var authentication = serviceCollection.GetInstance<IAuthenticationContext>();
                return new GrpcSpaceConnection(transport, _options, roots, entries, content, properties, authentication);
            });

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
