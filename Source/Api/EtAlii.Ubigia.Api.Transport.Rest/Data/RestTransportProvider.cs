namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using EtAlii.xTechnology.Threading;

    public class RestTransportProvider : ITransportProvider
    {
        private readonly IInfrastructureClient _infrastructureClient;

        private RestTransportProvider(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public static RestTransportProvider Create(IContextCorrelator contextCorrelator)
        {
            var httpClientFactory = new DefaultHttpClientFactory(contextCorrelator);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return new RestTransportProvider(infrastructureClient);
        }

        public static RestTransportProvider Create(IInfrastructureClient infrastructureClient)
        {
	        return new(infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new RestSpaceTransport(address, _infrastructureClient);
        }
    }
}
