// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal partial class SystemContentDataClient : SystemSpaceClientBase, IContentDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemContentDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }
    }
}
