namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.xTechnology.Threading;

    public class RestStorageTransportProvider : IStorageTransportProvider
    {
        private readonly IRestInfrastructureClient _infrastructureClient;

        private RestStorageTransportProvider(IRestInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public static RestStorageTransportProvider Create(IContextCorrelator contextCorrelator)
        {
            var httpClientFactory = new DefaultHttpClientFactory(contextCorrelator);
            var infrastructureClient = new RestInfrastructureClient(httpClientFactory);
            return new RestStorageTransportProvider(infrastructureClient);
        }

	    public static RestStorageTransportProvider Create(IRestInfrastructureClient infrastructureClient)
	    {
		    return new(infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new RestSpaceTransport(address, _infrastructureClient);
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new RestStorageTransport(address, _infrastructureClient);
        }
    }
}
