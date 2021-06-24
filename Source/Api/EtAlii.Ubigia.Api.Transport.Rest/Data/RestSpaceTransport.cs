// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class RestSpaceTransport : SpaceTransportBase, IRestSpaceTransport
    {
        private readonly IRestInfrastructureClient _infrastructureClient;

        public RestSpaceTransport(Uri address, IRestInfrastructureClient infrastructureClient)
            : base(address)
        {
            _infrastructureClient = infrastructureClient;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new RestSpaceClientsScaffolding(_infrastructureClient),
            };
        }
    }
}
