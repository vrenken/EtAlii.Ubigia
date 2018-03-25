namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    public class WebApiTransportProvider : ITransportProvider
    {
        private readonly IInfrastructureClient _infrastructureClient;

        private WebApiTransportProvider(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public static WebApiTransportProvider Create()//IInfrastructureClient infrastructureClient)
        {
	        return new WebApiTransportProvider(null);//infrastructureClient);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new WebApiSpaceTransport(_infrastructureClient);
        }
    }
}