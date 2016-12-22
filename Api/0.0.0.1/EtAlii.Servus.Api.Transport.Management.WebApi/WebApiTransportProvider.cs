namespace EtAlii.Servus.Api.Transport.WebApi
{
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Management.WebApi;

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