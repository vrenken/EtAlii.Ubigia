namespace EtAlii.Servus.Api.Management.WebApi
{
    using EtAlii.xTechnology.MicroContainer;
    using Transport;
    public class WebApiStorageTransport : StorageTransportBase, IWebApiStorageTransport
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public WebApiStorageTransport(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new WebApiStorageClientsScaffolding(_infrastructureClient),
            };
        }
    }
}
