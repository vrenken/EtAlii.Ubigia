namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class WebApiStorageTransportProvider : IStorageTransportProvider
    {
        private readonly IInfrastructureClient _infrastructureClient;

        private WebApiStorageTransportProvider(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

	    public static WebApiStorageTransportProvider Create(IInfrastructureClient infrastructureClient)
	    {
		    return new WebApiStorageTransportProvider(infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new WebApiSpaceTransport(_infrastructureClient);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new WebApiStorageTransport(_infrastructureClient);
        }
    }
}