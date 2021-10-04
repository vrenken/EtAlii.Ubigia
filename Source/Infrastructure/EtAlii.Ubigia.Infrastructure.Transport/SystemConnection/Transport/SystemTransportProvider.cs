// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemTransportProvider : IStorageTransportProvider
    {
        private readonly IInfrastructure _infrastructure;

        public SystemTransportProvider(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        /// <inheritdoc />
        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new SystemSpaceTransport(address, _infrastructure);
        }

        /// <inheritdoc />
        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new SystemStorageTransport(address, _infrastructure);
        }

        public IStorageTransport GetStorageTransport()
        {
            var serviceDetails = _infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

            return new SystemStorageTransport(serviceDetails.ManagementAddress, _infrastructure);
        }
    }
}
