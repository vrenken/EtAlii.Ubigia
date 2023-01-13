// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using EtAlii.Ubigia.Api.Transport;
using EtAlii.xTechnology.MicroContainer;

public class SystemConnectionScaffolding : IScaffolding
{
    private readonly SystemConnectionOptions _options;

    public SystemConnectionScaffolding(SystemConnectionOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public void Register(IRegisterOnlyContainer container)
    {
        var factoryMethod = _options.FactoryExtension ?? CreateSystemConnection;
        container.Register(factoryMethod);
    }

    private ISystemConnection CreateSystemConnection()
    {
        var hasTransportProvider = _options.TransportProvider != null;
        if (!hasTransportProvider)
        {
            throw new InvalidInfrastructureOperationException("Error creating system connection: No TransportProvider provided.");
        }

        if (_options.ServiceDetails == null)
        {
            throw new NotSupportedException("The service details are required to construct a SystemConnection instance");
        }

        return new SystemConnection(_options);
    }
}
