﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using EtAlii.Ubigia.Api.Transport;
using EtAlii.xTechnology.MicroContainer;

internal class SystemClientsScaffolding : IScaffolding
{
    private readonly IFunctionalContext _functionalContext;
    private readonly SpaceConnectionOptions _options;

    public SystemClientsScaffolding(IFunctionalContext functionalContext, SpaceConnectionOptions options = null)
    {
        _functionalContext = functionalContext;
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        if (_options != null)
        {
            container.Register<ISpaceConnection>(serviceCollection =>
            {
                var transport = serviceCollection.GetInstance<ISpaceTransport>();
                var roots = serviceCollection.GetInstance<IRootContext>();
                var entries = serviceCollection.GetInstance<IEntryContext>();
                var content = serviceCollection.GetInstance<IContentContext>();
                var properties = serviceCollection.GetInstance<IPropertiesContext>();
                var authentication = serviceCollection.GetInstance<IAuthenticationContext>();
                return new SystemSpaceConnection(transport, _options, roots, entries, content, properties, authentication);
            });
        }
        container.Register<IStorageConnection, SystemStorageConnection>();

        container.Register(() => _functionalContext);

        // Data clients.
        container.Register<IAuthenticationDataClient, SystemAuthenticationDataClient>();

        container.Register<IInformationDataClient, SystemInformationDataClient>();

        container.Register<IEntryDataClient, SystemEntryDataClient>();
        container.Register<IRootDataClient, SystemRootDataClient>();
        container.Register<IPropertiesDataClient, SystemPropertiesDataClient>();
        container.Register<IContentDataClient, SystemContentDataClient>();

        // Only management data clients as we do not have any management notification clients (yet).
        container.Register<IAuthenticationManagementDataClient, SystemAuthenticationManagementDataClient>();

        container.Register<IStorageDataClient, SystemStorageDataClient>();
        container.Register<IAccountDataClient, SystemAccountDataClient>();
        container.Register<ISpaceDataClient, SystemSpaceDataClient>();
    }
}
