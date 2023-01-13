// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using EtAlii.xTechnology.MicroContainer;

internal class DataConnectionScaffolding : IScaffolding
{
    private readonly DataConnectionOptions _options;

    public DataConnectionScaffolding(DataConnectionOptions options)
    {
        var hasTransportProvider = options.TransportProvider != null;
        if (!hasTransportProvider)
        {
            throw new InvalidInfrastructureOperationException("Error creating data connection: No TransportProvider provided.");
        }

        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        var factoryMethod = _options.FactoryExtension ?? (() => new DataConnection(_options));

        container.Register(() => _options.ConfigurationRoot);
        container.Register(() => factoryMethod());
    }
}
