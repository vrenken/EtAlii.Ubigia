namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiStorageTransport : StorageTransportBase, IWebApiStorageTransport
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public WebApiStorageTransport(Uri address, IInfrastructureClient infrastructureClient)
            : base(address)
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
