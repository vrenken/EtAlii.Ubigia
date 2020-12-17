namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.xTechnology.Threading;

    public class WebApiStorageTransportProvider : IStorageTransportProvider
    {
        private readonly IInfrastructureClient _infrastructureClient;

        private WebApiStorageTransportProvider(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public static WebApiStorageTransportProvider Create(IContextCorrelator contextCorrelator)
        {
            var httpClientFactory = new DefaultHttpClientFactory(contextCorrelator);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return new WebApiStorageTransportProvider(infrastructureClient);
        }

	    public static WebApiStorageTransportProvider Create(IInfrastructureClient infrastructureClient)
	    {
		    return new(infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new WebApiSpaceTransport(address, _infrastructureClient);
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new WebApiStorageTransport(address, _infrastructureClient);
        }
    }
}
