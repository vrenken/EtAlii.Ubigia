namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class RestSpaceTransport : SpaceTransportBase, IRestSpaceTransport
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public RestSpaceTransport(Uri address, IInfrastructureClient infrastructureClient)
            : base(address)
        {
            _infrastructureClient = infrastructureClient;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new RestSpaceClientsScaffolding(_infrastructureClient),
            };
        }
    }
}
