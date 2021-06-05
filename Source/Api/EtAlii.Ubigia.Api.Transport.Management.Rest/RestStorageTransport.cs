namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.xTechnology.MicroContainer;

    public class RestStorageTransport : StorageTransportBase, IRestStorageTransport
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public RestStorageTransport(Uri address, IInfrastructureClient infrastructureClient)
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
