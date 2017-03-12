namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.xTechnology.MicroContainer;

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
