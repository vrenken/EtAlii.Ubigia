namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.xTechnology.MicroContainer;

    public class RestStorageTransport : StorageTransportBase, IRestStorageTransport
    {
        private readonly IRestInfrastructureClient _infrastructureClient;

        public RestStorageTransport(Uri address, IRestInfrastructureClient infrastructureClient)
            : base(address)
        {
            _infrastructureClient = infrastructureClient;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new RestStorageClientsScaffolding(_infrastructureClient),
            };
        }
    }
}
