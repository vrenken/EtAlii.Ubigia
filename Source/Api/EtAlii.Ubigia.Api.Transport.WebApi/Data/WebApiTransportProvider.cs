namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;

    public class WebApiTransportProvider : ITransportProvider
    {
        private readonly IInfrastructureClient _infrastructureClient;

        private WebApiTransportProvider(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public static WebApiTransportProvider Create()
        {
            var httpClientFactory = new DefaultHttpClientFactory();
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
            return new WebApiTransportProvider(infrastructureClient);
        }

        public static WebApiTransportProvider Create(IInfrastructureClient infrastructureClient)
        {
	        return new(infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new WebApiSpaceTransport(address, _infrastructureClient);
        }
    }
}
