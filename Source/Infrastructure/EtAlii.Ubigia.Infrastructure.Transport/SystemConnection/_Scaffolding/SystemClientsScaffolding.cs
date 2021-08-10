// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    internal class SystemClientsScaffolding : IScaffolding
    {
        private readonly IInfrastructure _infrastructure;

        public SystemClientsScaffolding(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<ISpaceConnection, SystemSpaceConnection>();
            container.Register<IStorageConnection, SystemStorageConnection>();

            container.Register(() => _infrastructure);

            // Data clients.
            container.Register<IAuthenticationDataClient, SystemAuthenticationDataClient>();

            container.Register<IInformationDataClient, SystemInformationDataClient>();

            container.Register<IEntryDataClient, SystemEntryDataClient>();
            container.Register<IEntryNotificationClient, EntryNotificationClientStub>();

            container.Register<IRootDataClient, SystemRootDataClient>();
            container.Register<IRootNotificationClient, RootNotificationClientStub>();

            container.Register<IPropertiesDataClient, SystemPropertiesDataClient>();
            container.Register<IPropertiesNotificationClient, PropertiesNotificationClientStub>();

            container.Register<IContentDataClient, SystemContentDataClient>();
            container.Register<IContentNotificationClient, ContentNotificationClientStub>();

            // Only management data clients as we do not have any management notification clients (yet).
            container.Register<IAuthenticationManagementDataClient, SystemAuthenticationManagementDataClient>();

            container.Register<IStorageDataClient, SystemStorageDataClient>();
            container.Register<IAccountDataClient, SystemAccountDataClient>();
            container.Register<ISpaceDataClient, SystemSpaceDataClient>();

            // No Notification clients yet.
            container.Register<IStorageNotificationClient, StorageNotificationClientStub>();
            container.Register<IAccountNotificationClient, AccountNotificationClientStub>();
            container.Register<ISpaceNotificationClient, SpaceNotificationClientStub>();

        }
    }
}
