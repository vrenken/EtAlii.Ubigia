// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using EtAlii.xTechnology.Threading;

    public class RestTransportProvider : ITransportProvider
    {
        private readonly IRestInfrastructureClient _infrastructureClient;

        private RestTransportProvider(IRestInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public static RestTransportProvider Create(IContextCorrelator contextCorrelator)
        {
            var httpClientFactory = new DefaultHttpClientFactory(contextCorrelator);
            var infrastructureClient = new RestInfrastructureClient(httpClientFactory);
            return new RestTransportProvider(infrastructureClient);
        }

        public static RestTransportProvider Create(IRestInfrastructureClient infrastructureClient)
        {
	        return new(infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new RestSpaceTransport(address, _infrastructureClient);
        }
    }
}
