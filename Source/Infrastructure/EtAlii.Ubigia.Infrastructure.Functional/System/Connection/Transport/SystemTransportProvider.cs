// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;

    public class SystemTransportProvider : IStorageTransportProvider
    {
        private readonly IFunctionalContext _functionalContext;

        public SystemTransportProvider(IFunctionalContext functionalContext)
        {
            _functionalContext = functionalContext;
        }

        /// <inheritdoc />
        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new SystemSpaceTransport(address, _functionalContext);
        }

        /// <inheritdoc />
        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new SystemStorageTransport(address, _functionalContext);
        }

        public IStorageTransport GetStorageTransport()
        {
            var serviceDetails = _functionalContext.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

            return new SystemStorageTransport(serviceDetails.ManagementAddress, _functionalContext);
        }
    }
}
